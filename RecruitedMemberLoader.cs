using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitedMemberLoader : MonoBehaviour
{
    RecruitmentManager recruitmentManager;

    // Start is called before the first frame update
    void Start()
    {
        recruitmentManager = GameObject.Find("RecruitmentManager").GetComponent<RecruitmentManager>();

        // Change sprites in child GameObjects based on recruited NPC data
        ChangeSpritesInChildren();
        ChangeNpcdataInDialogueTrigger();

    }

    // Function to change sprites in child GameObjects
    void ChangeSpritesInChildren()
    {
        // Iterate through all child GameObjects
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the recruited NPC data for the current child index
            NPCData npcdata = recruitmentManager.recruitedNPCData[i];

            // Get the SpriteRenderer component of the current child
            SpriteRenderer spriteRenderer = transform.GetChild(i).GetComponent<SpriteRenderer>();

            // Check if the child has a SpriteRenderer component
            if (spriteRenderer != null && npcdata != null)
            {
                // Change the sprite in the SpriteRenderer based on the recruited NPC data
                spriteRenderer.sprite = npcdata.npcSprite;
            }
        }
    }

    // Function to change sprites in child GameObjects
    void ChangeNpcdataInDialogueTrigger()
    {
        // Iterate through all child GameObjects
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the recruited NPC data for the current child index
            NPCData npcdata = recruitmentManager.recruitedNPCData[i];

            // Get the SpriteRenderer component of the current child
            DialogueTrigger dialogueTrigger = transform.GetChild(i).GetComponent<DialogueTrigger>();

            // Check if the child has a SpriteRenderer component
            if (dialogueTrigger != null && npcdata != null)
            {
                dialogueTrigger.npcData = npcdata;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}