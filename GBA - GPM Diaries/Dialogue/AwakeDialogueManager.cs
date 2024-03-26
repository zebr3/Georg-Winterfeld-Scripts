using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using TMPro;
using Ink.Runtime;
//using UnityEngine.Audio;
using UnityEngine.EventSystems;


public class AwakeDialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] bool dontDestroyOnLoad;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    // [SerializeField] private GameObject uiElementsToHide;
    //  [SerializeField] private GameObject darkBox;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
   

    //RecruitmentManager recruitmentManager;
    //NPCController npcController;
    /*[Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    private DialogueAudioInfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    [SerializeField] private bool makePredictable;
    [SerializeField] private AudioMixerGroup mixerGroup;

    private AudioSource audioSource;*/

  
    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private Coroutine displayLineCoroutine;

    private bool awakeCanContinueToNextLine = false;

    private static AwakeDialogueManager awakeDialogueManager;

    

    private DialogueVariables dialogueVariables;

    private int npcDialogueIndex;

    private void Awake()
    {
        // dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        if (awakeDialogueManager != null)
        {
            Debug.LogWarning("More than one Dialogue in the scene");
        }
        awakeDialogueManager = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);

    }

    public static AwakeDialogueManager GetInstance()
    {
        return awakeDialogueManager;
    }

    private void Start()
    {
        if (dontDestroyOnLoad == true)
        {
            DontDestroyOnLoad(this);
        }

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

    }

    private void Update()
    {
        

    if (awakeCanContinueToNextLine == true && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)))
        {
            AwakeContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {

        currentStory = new Story(inkJSON.text);


        currentStory.BindExternalFunction("recruitNPC", (string targetName) => {

            NPCController currentNPC = GetCurrentNPC(targetName);
            if (currentNPC != null)
            {

                currentNPC.TryRecruit();
            }
        });

        currentStory.BindExternalFunction("DivertToKnotByNpcIndex", () => {
            // Your logic to determine the int value
            int npcInt = SetNpcDialogueIndex(npcDialogueIndex);

            // Set a variable in the Ink story to hold the value
            currentStory.variablesState["npcDialogueIndex"] = npcInt;

            // Return a dummy value (it's ignored by Ink)
            return 0;
        });

        currentStory.BindExternalFunction("activateItem", (string itemToActivate) =>
        {
            ObjectActivator objectActivator = FindObjectOfType<ObjectActivator>();
            objectActivator.ActivateObject(itemToActivate);

        });
        currentStory.BindExternalFunction("deactivateItem", (string itemToDeactivate) =>
        {
            ObjectActivator objectActivator = FindObjectOfType<ObjectActivator>();
            objectActivator.DeactivateObject(itemToDeactivate);

        });
        currentStory.BindExternalFunction("loadSceneWithFade", (string sceneName) =>
        {
            Debug.Log("Attempting to load scene: " + sceneName);

            LevelTransition levelTransition = FindObjectOfType<LevelTransition>();
            if (levelTransition != null)
            {
                levelTransition.sceneName = sceneName;
                levelTransition.FadeLoadScene(levelTransition.sceneName);
            }
            else
            {
                Debug.LogError("LevelTransition not found in the scene. Make sure it is present.");
            }


        });

        /*
        currentStory.BindExternalFunction("checkIfRecruited", (string targetName) => {
            // Assuming you have a method to get the NPC controller by name
            NPCController npcController = GetCurrentNPC(targetName);

            if (npcController != null)
            {
                if (npcController.IsRecruited)
                {
                    currentStory.variablesState["isNpcRecruited"] = true;
                }
                else
                {
                    currentStory.variablesState["isNpcRecruited"] = false;
                }
            }
        });*/


        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);


        dialogueVariables.StartListening(currentStory);





        AwakeContinueStory();

    }

    public int SetNpcDialogueIndex(int npcInt)
    {
        npcDialogueIndex = npcInt;
        Debug.Log("Npc Dialogue Index is: " + npcDialogueIndex);

        return npcDialogueIndex;

    }

    private NPCController GetCurrentNPC(string targetName)
    {
        // Find all NPCControllers in the scene
        NPCController[] npcControllers = FindObjectsOfType<NPCController>();

        foreach (NPCController npc in npcControllers)
        {
            // Check if the NPC's npcData name matches the target name
            if (npc.npcData != null && npc.npcData.npcName == targetName && !npc.IsRecruited)
            {
                return npc;
            }
        }

        return null; // No eligible NPC found
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        currentStory.UnbindExternalFunction("recruitNPC");
        currentStory.UnbindExternalFunction("DivertToKnotByNpcIndex");
        currentStory.UnbindExternalFunction("activateItem");
        currentStory.UnbindExternalFunction("deactivateItem");
        currentStory.UnbindExternalFunction("loadSceneWithFade");
        // currentStory.UnbindExternalFunction("checkIfRecruited");
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        // darkBox.SetActive(false);

        dialogueText.text = "";
       

        currentStory = null;
        // SetCurrentAudioInfo(defaultAudioInfo.id);

    }

    private void AwakeContinueStory()
    {
        Debug.Log("AwakeContinueStory called. CurrentStory is: " + (currentStory != null ? "not null" : "null"));


        if (currentStory != null)
        {
            if (currentStory.canContinue)
            {
                // set text for the current dialogue line
                if (displayLineCoroutine != null)
                {
                    StopCoroutine(displayLineCoroutine);
                }
                string nextLine = currentStory.Continue();
                HandleTags(currentStory.currentTags);
                Debug.Log("'" + nextLine + "'");

                // ---- ADD THIS
                // handle the random case where there's a new line for some reason
                while (nextLine.Equals("\n") && currentStory.canContinue)
                {
                    Debug.Log("Blank Line Skipped");
                    nextLine = currentStory.Continue();
                }
                // ----

                // handle case where the last line is an external function
                if (nextLine.Equals("") && !currentStory.canContinue)
                {
                    StartCoroutine(ExitDialogueMode());
                }
                // otherwise, handle the normal case for continuing the story
                else
                {
                    // handle tags
                    HandleTags(currentStory.currentTags);
                    displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
                }
            }
            else
            {
                StartCoroutine(ExitDialogueMode());
            }
        } else
        {
            Debug.Log("AwakeContinueStory called, but currentStory is null.");
        }
    }
    
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);


        awakeCanContinueToNextLine = false;

        foreach (char letter in line.ToCharArray())
        {

            if (Input.GetKey(KeyCode.Mouse1))
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;

            }

            else
            {
                // PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);

            }
        }

        continueIcon.SetActive(true);



        awakeCanContinueToNextLine = true;
    }
    /*
    private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
    {
        AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
        int frequencyLevel = currentAudioInfo.frequencyLevel;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool stopAudioSource = currentAudioInfo.stopAudioSource;

        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }
            AudioClip soundClip = null;

            if (makePredictable)
            {
                int hashCode = currentCharacter.GetHashCode();
                int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
                soundClip = dialogueTypingSoundClips[predictableIndex];
                int minPitchInt = (int)(minPitch * 100);
                int maxPitchInt = (int)(maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;
                if (pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = predictablePitch;
                }
                else
                {
                    audioSource.pitch = minPitch;
                }
            }
            else
            {
                int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
                soundClip = dialogueTypingSoundClips[randomIndex];
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            }

            audioSource.PlayOneShot(soundClip);
        }
    }
    */



    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

        }
    }
    /* case AUDIO_TAG:
         SetCurrentAudioInfo(tagValue);
         break;
     //case LAYOUT_TAG:
     //  break;
     default:
         Debug.LogWarning("Tag came in but is not currently being handled:" + tag);
         break;
 }
} 
} */

    
}

