using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDisplay : MonoBehaviour
{
    RecruitmentManager recruitmentManager;

    public bool changeNpcDataInObject;
    public bool changeNameInObject;

    // Start is called before the first frame update
    void Start()
    {
        recruitmentManager = GameObject.Find("RecruitmentManager").GetComponent<RecruitmentManager>();

        // Change sprites in child GameObjects based on recruited NPC data
        ChangeSpritesInChildren();

        if(changeNpcDataInObject == true)
        {
            ChangeNpcDatasInChildren();
        }

        if(changeNameInObject == true)
        {
            ChangeNamesInChildren();
        }
            
        
    }

    // Function to change sprites in child GameObjects
    void ChangeSpritesInChildren()
    {
        // Iterate through all child GameObjects
        for (int i = 0; i < transform.childCount; i++)
        {
            // Check if the index is within the bounds of the recruitedNPCData list
            if (i < recruitmentManager.recruitedNPCData.Count)
            {
                // Get the recruited NPC data for the current child index
                NPCData npcdata = recruitmentManager.recruitedNPCData[i];

                // Get the current child GameObject
                Transform child = transform.GetChild(i);

                // Iterate through all grandchildren of the current child
                for (int j = 0; j < child.childCount; j++)
                {
                    // Get the SpriteRenderer component of the current grandchild
                    Image image = child.GetChild(j).GetComponent<Image>();


                    // Check if the grandchild has a SpriteRenderer component
                    if (image != null && npcdata != null)
                    {
                        // Change the sprite in the SpriteRenderer based on the recruited NPC data
                        image.sprite = npcdata.portrait;
                    }
                }
            }
        }
    }

    void ChangeNamesInChildren()
    {
        // Iterate through all child GameObjects
        for (int i = 0; i < transform.childCount; i++)
        {
            // Check if the index is within the bounds of the recruitedNPCData list
            if (i < recruitmentManager.recruitedNPCData.Count)
            {
                // Get the recruited NPC data for the current child index
                NPCData npcdata = recruitmentManager.recruitedNPCData[i];

                // Get the current child GameObject
                Transform child = transform.GetChild(i);

                // Iterate through all grandchildren of the current child
                for (int j = 0; j < child.childCount; j++)
                {
                    // Get the SpriteRenderer component of the current grandchild
                    TMP_Text text = child.GetChild(j).GetComponent<TMP_Text>();


                    // Check if the grandchild has a SpriteRenderer component
                    if (text != null && npcdata != null)
                    {
                        // Change the sprite in the SpriteRenderer based on the recruited NPC data
                        text.text = npcdata.name;
                    }
                }
            }
        }
    }

    void ChangeNpcDatasInChildren()
    {
        // Iterate through all child GameObjects
        for (int i = 0; i < transform.childCount; i++)
        {
            // Check if the index is within the bounds of the recruitedNPCData list
            if (i < recruitmentManager.recruitedNPCData.Count)
            {
                // Get the recruited NPC data for the current child index
                NPCData npcdata = recruitmentManager.recruitedNPCData[i];

                // Get the current child GameObject
                Transform child = transform.GetChild(i);

                // Iterate through all grandchildren of the current child
                for (int j = 0; j < child.childCount; j++)
                {
                    // Get the OrganigrammSlots component of the current grandchild
                    OrganigrammSlots organigrammSlots = child.GetChild(j).GetComponent<OrganigrammSlots>();

                    // Check if the grandchild has an OrganigrammSlots component
                    if (organigrammSlots != null)
                    {
                        // Change the npcData in OrganigrammSlots based on the recruited NPC data
                        organigrammSlots.npcData = npcdata;
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Add any update logic here if needed
    }
}