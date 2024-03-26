using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoPlayingTriggerOnAwake : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField]  TextAsset inkJSON;
    public DialogueManager dialogueManager;

    private void Update()
    {
        dialogueManager = DialogueManager.GetInstance(); // Find the existing DialogueManager instance
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene!");
            return;
        }

        Debug.Log("DialogueManager found: " + dialogueManager.name);

        triggerDialogue();
    }


    private void triggerDialogue()
    {
        if (dialogueManager != null)
        {
            dialogueManager.EnterDialogueMode(inkJSON);
        }
    }
}
