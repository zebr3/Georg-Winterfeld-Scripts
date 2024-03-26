using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitedMemberLoader : MonoBehaviour
{
    //RecruitmentManager recruitmentManager;
    [SerializeField]  RecruitedNpcScriptableObject teamData;

    // Start is called before the first frame update
    void Start()
    {
        //recruitmentManager = GameObject.Find("RecruitmentManager").GetComponent<RecruitmentManager>();

        // Change sprites in child GameObjects based on recruited NPC data
       // ChangeSpritesInChildren();
       // ChangeNpcdataInDialogueTrigger();
    }

    // Function to change sprites in child GameObjects
    void ChangeSpritesInChildren()
    {
        // Iterate up to the minimum of the list count and the child count
        int count = Mathf.Min(teamData.team.Count, transform.childCount);

        for (int i = 0; i < count; i++)
        {
            // Get the recruited NPC data for the current child index
            NPCData npcdata = teamData.team[i];

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

    // Function to change NPC data in DialogueTrigger components
    void ChangeNpcdataInDialogueTrigger()
    {
        // Iterate up to the minimum of the list count and the child count
        int count = Mathf.Min(teamData.team.Count, transform.childCount);

        for (int i = 0; i < count; i++)
        {
            // Get the recruited NPC data for the current child index
            NPCData npcdata = teamData.team[i];

            // Get the DialogueTrigger component of the current child
            DialogueTrigger dialogueTrigger = transform.GetChild(i).GetComponent<DialogueTrigger>();

            // Check if the child has a DialogueTrigger component
            if (dialogueTrigger != null && npcdata != null)
            {
                dialogueTrigger.npcData = npcdata;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpritesInChildren();
        ChangeNpcdataInDialogueTrigger();
    }
}
