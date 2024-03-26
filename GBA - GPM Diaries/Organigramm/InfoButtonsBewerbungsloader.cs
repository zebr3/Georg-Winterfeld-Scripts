using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoButtonsBewerbungsloader : MonoBehaviour
{
    [SerializeField] OrganigrammSlots organigrammSlot;
    [SerializeField] GameObject infoPage;
    //[SerializeField] RecruitmentManager loaderRecruitmentManager;
    //[SerializeField] Image portraitImage;
    //[SerializeField] TextMeshProUGUI nameText;
    //[SerializeField] TextMeshProUGUI professionText;
    //[SerializeField] TextMeshProUGUI descriptionText;
    private static OrganigrammSlots lastPressedOrganigrammSlot;
    // [SerializeField] RecruitmentManager recruitedTeam;
    //[SerializeField] GameObject gameOverPanel;

    //public int currentNpcIndex = 0; // Index of the currently displayed NPC
    private static NPCData currentNpcData;
    // Start is called before the first frame update


    public void Start()
    {
        // loaderrecruitmentManager = GameObject.Find("RecruitmentManager").GetComponent<RecruitmentManager>();

        // Display data for the first NPC
        DisplayNPCData();
    }
    public void SetCurrentNPCData(OrganigrammSlots organigrammSlot)
    {
        Debug.Log("SetCurrentNPCData called");

        // Reset lastPressedOrganigrammSlot if it's a new instance
        if (lastPressedOrganigrammSlot != organigrammSlot)
        {
            lastPressedOrganigrammSlot = null;
        }

        if (infoPage.activeSelf)
        {
            if (lastPressedOrganigrammSlot == organigrammSlot)
            {
                // If the same button is clicked again, toggle the visibility
                infoPage.SetActive(!infoPage.activeSelf);
            }
            else
            {
                // If a different button is clicked, show it and set the NPC data
                infoPage.SetActive(true);
                currentNpcData = organigrammSlot.npcData;
                lastPressedOrganigrammSlot = organigrammSlot;
                DisplayNPCData();
            }
        }
        else
        {
            // If the infoPage is not active, show it and set the NPC data
            infoPage.SetActive(true);
            currentNpcData = organigrammSlot.npcData;
            lastPressedOrganigrammSlot = organigrammSlot;
            DisplayNPCData();
        }
    }


    // Function to display NPC data
    public void DisplayNPCData()
    {
        // Check if the current index is within the valid range
        if (currentNpcData != null)
        {
            NPCData npcdata = organigrammSlot.npcData;

            // Change the UI elements based on the recruited NPC data

            if (infoPage != null && npcdata != null)
            {
                infoPage.GetComponent<Image>().sprite = npcdata.bewerbungSprite;
            }
            /*
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
                
                professionText.text = npcdata.standardProfession;
            }

            if (descriptionText != null && npcdata != null)
            {
                descriptionText.text = npcdata.anschreibText;
            }
            */
        }
    }
}
