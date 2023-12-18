using System.Collections;
using UnityEngine;

public class AwakeDialogueTrigger : MonoBehaviour
{
   

    [SerializeField] TextAsset inkjson;

    public DialogueManager dialogueManager;
    public AwakeDialogueManager awakeDialogueManager;
    public bool awake;
    private void Start()
    {

        TriggerDialogue();
        
    }


    IEnumerator TriggerDialogueDelayed()
    {
        yield return 1; // Wait for one frame to ensure other Start methods have executed
        TriggerDialogue();
    }

    void Update()
    {
       

       
    }


    public void TriggerDialogue()
    {
        if(awake == true) 
        { 
            awakeDialogueManager.EnterDialogueMode(inkjson);
        } 
        else 
        {
            dialogueManager.EnterDialogueMode(inkjson);
        }
       
    }

}
