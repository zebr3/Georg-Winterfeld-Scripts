using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] GameObject postIt;
    [SerializeField] GameObject secondPostIt;

    public int currentNpcIndex = 0; // Index of the currently displayed NPC
    public NPCData currentNpcData;

    // Added boolean to control the source of NPC data
    public bool useMaybeStack = false;

    void Start()
    {
        // Display data for the first NPC
        DisplayNPCData();
    }

   
    public void DisplayNPCData()
    {
        if (useMaybeStack && loaderRecruitmentManager.maybeStack.Count > 0)
        {
            DisplayNPCDataFromMaybeStack();
        }
        else
        {
            DisplayNPCDataFromAllNPCData();
        }
    }


    private void DisplayNPCDataFromAllNPCData()
    {
        if (currentNpcIndex >= 0 && currentNpcIndex < loaderRecruitmentManager.allNPCData.Count)
        {
            NPCData npcdata = loaderRecruitmentManager.allNPCData[currentNpcIndex];
            UpdateUIElements(npcdata);
        }
    }

    public void DisplayNPCDataFromMaybeStack()
    {
        if (currentNpcIndex >= 0 && currentNpcIndex < loaderRecruitmentManager.maybeStack.Count)
        {
            NPCData npcdata = loaderRecruitmentManager.maybeStack[currentNpcIndex];
            UpdateUIElements(npcdata);
        }
    }

    private void UpdateUIElements(NPCData npcdata)
    {

        if (portraitImage != null && npcdata != null)
        {
            portraitImage.sprite = npcdata.bewerbungSprite;
        }

        /*
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
        */
    }

    // Method to switch between allNPCData and maybeStack
    public void ToggleUseMaybeStack(bool useMaybeStack)
    {
        this.useMaybeStack = useMaybeStack;

        // Reset currentNpcIndex to 0 when switching to allNPCData or if the maybeStack is empty
        if (!useMaybeStack || loaderRecruitmentManager.maybeStack.Count == 0)
        {
            currentNpcIndex = 0;
        }

        // Set currentNpcData based on the current state of useMaybeStack
        if (useMaybeStack && loaderRecruitmentManager.maybeStack.Count > 0)
        {
            postIt.SetActive(true);
            secondPostIt.SetActive(false);
            currentNpcData = loaderRecruitmentManager.maybeStack[currentNpcIndex];
        }
        else if (!useMaybeStack && loaderRecruitmentManager.allNPCData.Count > 0)
        {
            postIt.SetActive(false);
            
            currentNpcData = loaderRecruitmentManager.allNPCData[currentNpcIndex];
        }

        // Display data based on the current state of useMaybeStack
        DisplayNPCData();
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


    public void GoToNextNPC()
    {
        if (useMaybeStack)
        {
            postIt.SetActive(true);
            secondPostIt.SetActive(false);
            // Check if the maybeStack is not empty
            if (loaderRecruitmentManager.maybeStack.Count > 0)
            {
                // Increment the maybe stack index
                //currentNpcIndex++;

                // Check if the index exceeds the maybeStack size, and loop back to the beginning
                if (currentNpcIndex >= loaderRecruitmentManager.maybeStack.Count)
                {
                    currentNpcIndex = 0;
                }

                // Set currentNpcData based on the current state of useMaybeStack
                currentNpcData = loaderRecruitmentManager.maybeStack[currentNpcIndex];
            }
            else
            {
                // Switch back to allNPCData stack if maybeStack is empty
                postIt.SetActive(false);
                useMaybeStack = false;
                currentNpcIndex = 0;

                // Check if allNPCData stack is empty and toggle to maybeStack
                if (loaderRecruitmentManager.allNPCData.Count == 0)
                {
                    ToggleUseMaybeStack(true);
                    // Set currentNpcData based on the current state of useMaybeStack
                    currentNpcData = loaderRecruitmentManager.maybeStack.Count > 0
                        ? loaderRecruitmentManager.maybeStack[currentNpcIndex]
                        : null;
                }
            }
        }
        else
        {
            // Check if the index exceeds the allNPCData size, and loop back to the beginning
            if (currentNpcIndex >= loaderRecruitmentManager.allNPCData.Count)
            {
                // Switch to maybeStack if allNPCData is empty
                if (loaderRecruitmentManager.allNPCData.Count == 0)
                {
                    postIt.SetActive(true);
                    ToggleUseMaybeStack(true);
                    // Set currentNpcData based on the current state of useMaybeStack
                    currentNpcData = loaderRecruitmentManager.maybeStack.Count > 0
                        ? loaderRecruitmentManager.maybeStack[currentNpcIndex]
                        : null;
                }
                else
                {
                    postIt.SetActive(false);
                    currentNpcIndex = 0;
                    // Set currentNpcData based on the current state of useMaybeStack
                    currentNpcData = loaderRecruitmentManager.allNPCData[currentNpcIndex];
                }
            }
        }

        // Display data for the next NPC
        DisplayNPCData();
    }




    public void RecruitNPCs()
    {
        // Ensure that there are NPCs to recruit
        if (loaderRecruitmentManager.allNPCData.Count > 0)
        {
            // Get the current NPC data
            NPCData npcdata = loaderRecruitmentManager.allNPCData[currentNpcIndex];

            // Recruit the current NPC
            recruitedTeam.RecruitNPC(npcdata);

            // Increment the current NPC index after recruitment
            //currentNpcIndex++;

            // Check if the index exceeds the list size, and loop back to the beginning
            if (currentNpcIndex >= loaderRecruitmentManager.allNPCData.Count)
            {
                currentNpcIndex = 0;
            }

            // Display data for the next NPC
            DisplayNPCData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useMaybeStack && loaderRecruitmentManager.maybeStack.Count > 0 && currentNpcIndex < loaderRecruitmentManager.maybeStack.Count)
        {
            currentNpcData = loaderRecruitmentManager.maybeStack[currentNpcIndex];
        }
        else if (!useMaybeStack && loaderRecruitmentManager.allNPCData.Count > 0 && currentNpcIndex < loaderRecruitmentManager.allNPCData.Count)
        {
            currentNpcData = loaderRecruitmentManager.allNPCData[currentNpcIndex];
        }
        else if (loaderRecruitmentManager.allNPCData.Count == 0
            && loaderRecruitmentManager.maybeStack.Count == 0
            && loaderRecruitmentManager.recruitedNPCData.Count < 5)
        {
            // Game over condition
            // Debug.Log("Game Over!");
            gameOverPanel.SetActive(true);
        }
        // You can add additional update logic here if needed

        if(postIt.activeInHierarchy == true)
        {
            secondPostIt.SetActive(false);
        }
    }

    


}
