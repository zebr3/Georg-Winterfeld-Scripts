using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SimpleDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] TextMeshProUGUI speakerName;
    [SerializeField] Button continueButton;
    [SerializeField] Image portraitImage;
    //[SerializeField] Button optionsButton;

    [Header("Dialogue")]
    [TextArea]
    [SerializeField] string[] lines;
    [SerializeField] string[] speakerSet;
    [SerializeField] Sprite[] portrait;
    [SerializeField] public float textSpeed;
    [SerializeField] TextSpeed getTextSpeed;

    public int index;
    [SerializeField] RecruitedNpcScriptableObject teamData;
    [SerializeField] bool setDialogueCharacters;
    [SerializeField] bool addCard;
    [SerializeField] NPCData cardToAdd;
    [SerializeField] bool activateObject;
    [SerializeField] GameObject objectToActivate;
    [SerializeField] bool deactivateObject;
    [SerializeField] GameObject objectToDeactivate;
    [SerializeField] bool closeDialoguePanel;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject darkPanel;

    [Header("Scene Transition")]
    public bool changeScene;
    public string sceneName;
    public LevelTransition levelTransition;

    // Start is called before the first frame update
    void Start()
    {
        
       // textComponent.text = string.Empty;
        continueButton.gameObject.SetActive(false);
        StartDialogue();
       // recruitmentManager = GameObject.Find("RecruitmentManager").GetComponent<RecruitmentManager>();

       
    }

    // Update is called once per frame
    void Update()
    {
        textSpeed = getTextSpeed.speed;
        if (Input.GetMouseButtonDown(0)) // Right mouse button to skip the current line
        { 
                CompleteLine(); 
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        string line = lines[index];
        SetTextOpacity(0f);

        if(setDialogueCharacters == false)
        {

            // Replace placeholders with NPC names based on their professions
            foreach (NPCData npcData in teamData.team)
            {
                string placeholder = "{" + npcData.profession + "}";
                line = line.Replace(placeholder, npcData.npcName);
            }

        }
       

        CheckObjectActivation(line);

        // Replace the activation placeholder if it exists
        line = line.Replace("{objecttoactivate}", "");
        line = line.Replace("{objecttodeactivate}", "");
        line = line.Replace("{firstline}", "");

        // Display the text only if it doesn't contain the activation placeholder
        if (!string.IsNullOrEmpty(line.Trim()))
        {
            textComponent.text = line;

            if (setDialogueCharacters)
            {
                speakerName.text = speakerSet[index];
                portraitImage.sprite = portrait[index];
            }
            else
            {
                SetSpeaker(speakerSet[index]);
            }

            yield return new WaitForSeconds(textSpeed);

            int totalCharacters = line.Length;
            int visibleCharacters = 0;

            SetTextOpacity(100f);

            while (visibleCharacters <= totalCharacters)
            {
                textComponent.maxVisibleCharacters = visibleCharacters;
                visibleCharacters++;
                yield return new WaitForSeconds(textSpeed);
            }

            continueButton.gameObject.SetActive(true);
        }
        else
        {
            // If the line is empty after replacing the placeholder, move to the next line
            ContinueButtonPressed();
        }
    }
    private void SetSpeaker(string speaker)
    {
        bool speakerSetFound = false;

       
        if(setDialogueCharacters == false)
        {

            foreach (NPCData npcData in teamData.team)
            {
                if (npcData.profession.Equals(speaker, StringComparison.OrdinalIgnoreCase))
                {
                    speakerName.text = npcData.npcName;
                    portraitImage.sprite = npcData.portrait;
                    speakerSetFound = true;
                    break;
                }
            }
        }
        

        if (!speakerSetFound)
        {
            // If no matching NPC data is found, use the default values
            int speakerIndex = Array.FindIndex(speakerSet, s => s.Equals(speaker, StringComparison.OrdinalIgnoreCase));
            if (speakerIndex != -1 && speakerIndex < portrait.Length)
            {
                speakerName.text = speakerSet[speakerIndex];
                portraitImage.sprite = portrait[speakerIndex];
            }
            else
            {
                // If the speaker is not found in the speakerSet array, use the default values for the current index
                speakerName.text = speakerSet[index];
                portraitImage.sprite = portrait[index];
            }
        }
    }




    private void SetTextOpacity(float alpha)
    {
        Color currentColor = textComponent.color;
        currentColor.a = alpha;
        textComponent.color = currentColor;
    }

    public void ContinueButtonPressed()
    {
        continueButton.gameObject.SetActive(false);

       

       

        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);

            if (addCard == true)
            {
                teamData.specialCards.Add(cardToAdd);
            }

            if (changeScene)
            {
                if (!string.IsNullOrEmpty(sceneName))
                {
                    ChangeScene(sceneName);
                }
                else
                {
                    Debug.LogWarning("Scene name is not set for scene change.");
                }
            }

            if (closeDialoguePanel == true && dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
                darkPanel.SetActive(false);
            }
        }

        
    }

    private void CheckObjectActivation(string line)
    {
        string objectToActivatePlaceholder = "{objecttoactivate}";
        string objectToDeactivatePlaceholder = "{objecttodeactivate}";

        if (line.Contains(objectToActivatePlaceholder))
        {
            ActivateObject(objectToActivate);

        } 
        if (line.Contains(objectToDeactivatePlaceholder))
        {
            DeactivateObject(objectToDeactivate);
        }
    }

    private void ActivateObject(GameObject objectToActivate)
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }

    private void DeactivateObject(GameObject objectToDeactivate)
    {
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
    }

    void CompleteLine()
    {
        StopAllCoroutines();
        textComponent.maxVisibleCharacters = lines[index].Length;
        continueButton.gameObject.SetActive(true);
    }

    void ChangeScene(string sceneName)
    {
        levelTransition.FadedLoadScene(sceneName);
    }
}
