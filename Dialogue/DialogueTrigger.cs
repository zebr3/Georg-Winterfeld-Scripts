using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Player Distance")]
    [SerializeField] Transform playerTransform;
    [SerializeField] float lengthCheckForUI = 0.5f;
    [SerializeField] float lengthCheckForDialogue = 0.4f;

    [Header("Dialogue Objects")]
    [SerializeField] GameObject objectOverHead;
    [SerializeField] Sprite interactionIcon;
    [SerializeField] Sprite talkIcon;
    public Sprite objectSprite;
    public NPCData npcData;

    [SerializeField] TextAsset inkjson;

    [Header("Dialogue Trigger on Awake")]
    [SerializeField] bool dialogueTriggerOnAwake = false;
    private int dialogueTriggerCount = 0;

    DialogueManager dialogueManager;

    private void Start()
    {
        
        if (dialogueTriggerOnAwake == false)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;



            objectSprite = objectOverHead?.GetComponent<SpriteRenderer>()?.sprite;

            if (objectSprite == null)
            {
                Debug.LogError("ObjectSprite not set or not found. Please assign the object over head with a sprite renderer component.");
                return;
            }
        }

        if (dialogueTriggerOnAwake)
        {
            StartCoroutine(TriggerDialogueDelayed());
        }
    }

    private void Awake()
    {
        if (dialogueTriggerOnAwake == true)
        {
            TriggerDialogue();
            dialogueTriggerCount++;
        }
    }

    IEnumerator TriggerDialogueDelayed()
    {
        yield return 1; // Wait for one frame to ensure other Start methods have executed
        TriggerDialogue();
    }

    void Update()
    {
        dialogueManager = DialogueManager.GetInstance();

        if (playerTransform != null || objectOverHead != null || objectSprite != null)
        {
            Vector2 offsetToPlayer = transform.position - playerTransform.position;
            float squareDistanceToPlayer = offsetToPlayer.sqrMagnitude;
            objectOverHead.GetComponent<SpriteRenderer>().sprite = objectSprite;

            // Check if player is close enough to show the interaction icon.
            if (squareDistanceToPlayer <= lengthCheckForUI * lengthCheckForUI)
            {
                objectOverHead.SetActive(true);
                objectSprite = interactionIcon;
            }
            else
            {
                objectOverHead.SetActive(false);
            }

            // Check if player is close enough to initiate dialogue.
            if (squareDistanceToPlayer <= lengthCheckForDialogue * lengthCheckForDialogue)
            {
                objectSprite = talkIcon;

                if (dialogueTriggerOnAwake == false && Input.GetKeyDown(KeyCode.E) && dialogueManager.dialogueIsPlaying == false)
                {
                    TriggerDialogue();
                }
            }
        }
    }


    public void TriggerDialogue()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager is null.");
            return;
        }

        if (inkjson == null)
        {
            Debug.LogError("Inkjson is null.");
            return;
        }

        if (npcData != null)
        {
            dialogueManager.SetNpcDialogueIndex(npcData.npcDialogueIndex);
        }

        dialogueManager.EnterDialogueMode(inkjson);
    }

}
