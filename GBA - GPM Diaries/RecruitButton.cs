using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecruitButton : MonoBehaviour
{
    [SerializeField] RecruitmentManager recruitmentManager;
    [SerializeField] BewerbungsLoader bewerbungsLoader;
    [SerializeField] Animator stampAnimator;
    [SerializeField] GameObject stampObject;
    [SerializeField] Button recruitmentButton;
    [SerializeField] Button otherRecruitmentButton;
    [SerializeField] Button maybeButton;

    public void OnRecruitButtonClick()
    {
        // Check if there's any NPC left to recruit
        if ((recruitmentManager.allNPCData.Count > 0 || recruitmentManager.maybeStack.Count > 0) && recruitmentManager.recruitedNPCData.Count < 5)
        {
            PlayStampAnimation("stampApproved");

            // Recruit the current NPC based on whether Maybe Stack is active or not
            if (bewerbungsLoader.useMaybeStack)
            {
                recruitmentManager.RecruitNPCFromMaybeStack(bewerbungsLoader.currentNpcData);
            }
            else
            {
                recruitmentManager.RecruitNPC(bewerbungsLoader.currentNpcData);
            }

            // Remove the recruited NPC from the appropriate list
            if (bewerbungsLoader.useMaybeStack)
            {
                recruitmentManager.maybeStack.RemoveAt(bewerbungsLoader.currentNpcIndex);
            }
            else
            {
                recruitmentManager.allNPCData.RemoveAt(bewerbungsLoader.currentNpcIndex);
            }

            if (recruitmentManager.allNPCData.Count > 0 || recruitmentManager.maybeStack.Count > 0)
            {
                StartCoroutine(ShowNextNPCAfterDelay());
            }
            else
            {
                // Handle the game over scenario here.
            }
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
                // Move the current NPC to the end of the maybeStack
                recruitmentManager.maybeStack.Remove(bewerbungsLoader.currentNpcData);
                recruitmentManager.maybeStack.Add(bewerbungsLoader.currentNpcData);

                // Display the next NPC
                bewerbungsLoader.GoToNextNPC();

                // If using maybeStack, display the NPCData from the maybeStack
                if (bewerbungsLoader.useMaybeStack)
                {
                    bewerbungsLoader.DisplayNPCDataFromMaybeStack();
                }
            }
        }
    }




    public void OnDeclineButtonClick()
    {
        // Check if there's any NPC left to recruit
        if (recruitmentManager.allNPCData.Count > 0 || recruitmentManager.maybeStack.Count > 0)
        {
            PlayStampAnimation("stampAbgelehnt");

            if (bewerbungsLoader.useMaybeStack && recruitmentManager.maybeStack.Count > 0)
            {
                // Remove the declined NPC from the maybeStack list
                recruitmentManager.maybeStack.RemoveAt(bewerbungsLoader.currentNpcIndex);
            }
            else if (!bewerbungsLoader.useMaybeStack && recruitmentManager.allNPCData.Count > 0)
            {
                // Remove the declined NPC from the allNPCData list
                recruitmentManager.allNPCData.RemoveAt(bewerbungsLoader.currentNpcIndex);
            }

            StartCoroutine(ShowNextNPCAfterDelay());
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




    private void PlayStampAnimation(string animationName)
    {
        // Set the trigger parameter to play the specified animation
        stampAnimator.Play(animationName, 0, 0);
    }

    private System.Collections.IEnumerator ShowNextNPCAfterDelay()
    {
        int YourDelayInSeconds = 1;

        // Hide the button
        //stampObject.SetActive(true);
        // stampAnimator.Play("stampDefault");
        recruitmentButton.interactable = false;
        otherRecruitmentButton.interactable = false;
        maybeButton.interactable = false;
        // Wait for a specific duration before displaying the next NPC
        yield return new WaitForSeconds(YourDelayInSeconds);

        // Display the next NPC
        bewerbungsLoader.GoToNextNPC();
        recruitmentButton.interactable = true;
        otherRecruitmentButton.interactable = true;
        maybeButton.interactable = true;

        // Show the button again
        stampAnimator.Play("stampDefault");
    }

}
