using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BewerbungsLoader : MonoBehaviour
{
    [SerializeField] RecruitmentManager loaderRecruitmentManager;
    [SerializeField] Image portraitImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI professionText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] RecruitmentManager recruitedTeam;
    [SerializeField] GameObject gameOverPanel;

    public int currentNpcIndex = 0; // Index of the currently displayed NPC
    public NPCData currentNpcData;
    // Start is called before the first frame update
    void Start()
    {
       // loaderrecruitmentManager = GameObject.Find("RecruitmentManager").GetComponent<RecruitmentManager>();

        // Display data for the first NPC
        DisplayNPCData();
    }

    // Function to display NPC data
    public void DisplayNPCData()
    {
        // Check if the current index is within the valid range
        if (currentNpcIndex >= 0 && currentNpcIndex < loaderRecruitmentManager.allNPCData.Count)
        {
            NPCData npcdata = loaderRecruitmentManager.allNPCData[currentNpcIndex];

            // Change the UI elements based on the recruited NPC data
            if (portraitImage != null && npcdata != null)
            {
                portraitImage.sprite = npcdata.portrait;
            }

            if (nameText != null && npcdata != null)
            {
                nameText.text = npcdata.name;
            }

            if (professionText != null && npcdata != null)
            {
                professionText.text = npcdata.profession;
            }

            if (descriptionText != null && npcdata != null)
            {
                descriptionText.text = npcdata.anschreibText;
            }

            // Update the NPC data in the associated DialogueTrigger
            //UpdateNpcDataInDialogueTrigger(npcdata);
        } else
        {
            NPCData npcdata = loaderRecruitmentManager.maybeStack[currentNpcIndex];

            // Change the UI elements based on the recruited NPC data
            if (portraitImage != null && npcdata != null)
            {
                portraitImage.sprite = npcdata.portrait;
            }

            if (nameText != null && npcdata != null)
            {
                nameText.text = npcdata.name;
            }

            if (professionText != null && npcdata != null)
            {
                professionText.text = npcdata.profession;
            }

            if (descriptionText != null && npcdata != null)
            {
                descriptionText.text = npcdata.anschreibText;
            }
        }
    }

    public void DisplayNPCDataFromMaybeStack()
    {
        if (currentNpcIndex >= 0 && currentNpcIndex < loaderRecruitmentManager.maybeStack.Count)
        {
            NPCData npcdata = loaderRecruitmentManager.maybeStack[currentNpcIndex];

            // Update UI elements based on the NPC data
            if (portraitImage != null && npcdata != null)
            {
                portraitImage.sprite = npcdata.portrait;
            }

            if (nameText != null && npcdata != null)
            {
                nameText.text = npcdata.name;
            }

            if (professionText != null && npcdata != null)
            {
                professionText.text = npcdata.profession;
            }

            if (descriptionText != null && npcdata != null)
            {
                descriptionText.text = npcdata.anschreibText;
            }
        }
    }


    /*
    // Function to change NPC data in DialogueTrigger
    void UpdateNpcDataInDialogueTrigger(NPCData npcdata)
    {
        DialogueTrigger dialogueTrigger = transform.GetChild(currentNpcIndex).GetComponent<DialogueTrigger>();
        if (dialogueTrigger != null && npcdata != null)
        {
            dialogueTrigger.npcData = npcdata;
        }
    }*/

    // Method to manually go to the next NPC in the list
    public void GoToNextNPC()
    {
        // Increment the current NPC index
        currentNpcIndex++;

        // Check if the index exceeds the list size, and loop back to the beginning
        if (currentNpcIndex >= loaderRecruitmentManager.allNPCData.Count)
        {
            currentNpcIndex = 0;
        }

        // Display data for the next NPC
        DisplayNPCData();
    }

    public void RecruitNPCs()
    {

       
        NPCData npcdata = loaderRecruitmentManager.allNPCData[currentNpcIndex];
        
        recruitedTeam.RecruitNPC(npcdata);
        // Increment the current NPC index
        

        
        // Check if the index exceeds the list size, and loop back to the beginning
        if (currentNpcIndex >= loaderRecruitmentManager.allNPCData.Count)
        {
            currentNpcIndex = 0;
        }
        
        // Display data for the next NPC
        DisplayNPCData();
    }
    // Update is called once per frame
    void Update()
    {
        if (loaderRecruitmentManager.allNPCData.Count != 0)
        {
            currentNpcData = loaderRecruitmentManager.allNPCData[currentNpcIndex];
        } else if (loaderRecruitmentManager.maybeStack.Count != 0)
        {
            currentNpcData = loaderRecruitmentManager.maybeStack[currentNpcIndex];
        }
        else
        {
            Debug.Log("Game Over!");
            gameOverPanel.SetActive(true);
        }
        // You can add additional update logic here if needed
    }
}
