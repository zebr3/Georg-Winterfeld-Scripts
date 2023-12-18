using System.Linq;
using UnityEngine;

public class RecruitButton : MonoBehaviour
{
    [SerializeField] RecruitmentManager recruitmentManager;
    [SerializeField] BewerbungsLoader bewerbungsLoader;

    public void OnRecruitButtonClick()
    {
        // Check if there's any NPC left to recruit
        if (recruitmentManager.allNPCData.Count > 0 || recruitmentManager.maybeStack.Count > 0 && recruitmentManager.recruitedNPCData.Count < 5)
        {
            // Recruit the current NPC
            recruitmentManager.RecruitNPC(bewerbungsLoader.currentNpcData);

            if(recruitmentManager.allNPCData.Count > 0 ) 
            { 
                recruitmentManager.allNPCData.RemoveAt(bewerbungsLoader.currentNpcIndex); 
            } else
            {
                recruitmentManager.maybeStack.RemoveAt(bewerbungsLoader.currentNpcIndex);
            }
            
            

            // Display the next NPC
            bewerbungsLoader.GoToNextNPC();
        }
    }

    public void OnMaybeButtonClick()
    {
        // Check if there's any NPC left to recruit in allNPCData or maybeStack, and recruited NPCs are less than 5
        if ((recruitmentManager.allNPCData.Count > 0 || recruitmentManager.maybeStack.Count > 0) && recruitmentManager.recruitedNPCData.Count < 5)
        {
            // Check if the current NPC is not already in the maybeStack
            if (!recruitmentManager.maybeStack.Contains(bewerbungsLoader.currentNpcData))
            {
                // Recruit the current NPC
                recruitmentManager.MaybeStackNPC(bewerbungsLoader.currentNpcData);

                // Remove the recruited NPC from the allNPCData list
                recruitmentManager.allNPCData.RemoveAt(bewerbungsLoader.currentNpcIndex);

                // Display the next NPC
                bewerbungsLoader.GoToNextNPC();
            }
            else
            {
                // Handle the case where the NPC is already in the maybeStack
                Debug.Log("This NPC is already in the Maybe Stack!");
                // You can choose to display a message or take other actions as needed.
            }
        }
    }

    public void OnDeclineButtonClick()
    {
        // Check if there's any NPC left to recruit
        if (recruitmentManager.allNPCData.Count > 0 || recruitmentManager.maybeStack.Count > 0)
        {


            if (recruitmentManager.allNPCData.Count > 0)
            {
                recruitmentManager.allNPCData.RemoveAt(bewerbungsLoader.currentNpcIndex);
            }
            else
            {
                recruitmentManager.maybeStack.RemoveAt(bewerbungsLoader.currentNpcIndex);
            }
            // Remove the declined NPC from the allNPCData list
           
            // Display the next NPC
            bewerbungsLoader.GoToNextNPC();
        }

        // Check if allNPCData list is empty and conditions for game over are met
        if (recruitmentManager.allNPCData.Count <= 0 &&
            recruitmentManager.recruitedNPCData.Count < 5 &&
            recruitmentManager.maybeStack.Count <= 0)
        {
            Debug.Log("Game Over! Du kannst nicht alle ablehnen!");
        }
        // Check if allNPCData list is empty and there are NPCs in the maybeStack
        else if (recruitmentManager.allNPCData.Count <= 0 && recruitmentManager.maybeStack.Count > 0)
        {
            // Display NPCData in BewerbungsLoader from the maybeStack
            bewerbungsLoader.DisplayNPCDataFromMaybeStack();
        }
    }

}
