using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{

    // public GameObject hintObject; // The game object to activate as a hint.
    //public string playerTag = "Player"; // The tag of the player object.
    private float activationDistance = 0.35f; // The distance at which the hint object should be activated.
    public NPCData npcData;
    private bool isNearNPC = false;
    private Transform player;

    private DialogueTrigger dialogueTrigger;

    public float interactionRange = 2.0f; // The range at which the player can interact with the NPC
    public GameObject interactIcon;
    private RecruitmentManager recruitmentManager;
    private bool inRange = false;
    public bool isRecruited = false;

    void Start()
    {
        recruitmentManager = FindObjectOfType<RecruitmentManager>(); // Find the RecruitmentManager in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueTrigger = GetComponent<DialogueTrigger>();
        if (player == null)
        {
            Debug.LogError("Player not found in the scene");
        }
    }

    void Update()
    {
        if (player == null)
        {
            return; // Player not found; do nothing.
        }

        // Calculate the distance between the player and the NPC.
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && !isNearNPC)
        {
            // Player is within activation distance.
            inRange = true;
            // ActivateHintObject(true);
        }
        else if (distanceToPlayer > activationDistance && isNearNPC)
        {
            // Player moved away from the NPC.
            inRange = false;
            //ActivateHintObject(false);
        }
        if (inRange)
        {
            // Display an interaction icon or text above the NPC's head
            //interactIcon.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                TryRecruit();
            }
        }

        if(IsRecruited == true)
        {
           // dialogueTrigger.enabled = false;
            //interactIcon.SetActive(false);
        }
    }



    public void Recruit()
    {
        isRecruited = true;
        if(dialogueTrigger != null)
        {
            dialogueTrigger.enabled = false;
        }

        if (interactIcon != null)
        {
            interactIcon.SetActive(false);
        }
    }

    public bool IsRecruited
    {
        get { return isRecruited; }
    }

    public Sprite GetRecruitedSprite()
    {
      
            // Assuming you have a SpriteRenderer component attached to the NPC
            Sprite npcSprite = npcData.npcSprite;

            if (npcSprite != null)
            {
                // Return the sprite from the NPC's sprite renderer
                return npcSprite;
            }
            else
            {
                // Handle the case where the NPC doesn't have a SpriteRenderer component
                Debug.LogError("NPC does not have a SpriteRenderer component.");
                return null; // You can return a default sprite or handle it in a way that makes sense for your game.
            }

    }

    public string GetRecruitedName()
    {

        if (npcData != null)
        {
            // Return the sprite from the NPC's sprite renderer
            return npcData.npcName;
        }
        else
        {
            // Handle the case where the NPC doesn't have a SpriteRenderer component
            Debug.LogError("NPC does not have a NPCData attached to it.");
            return null; // You can return a default sprite or handle it in a way that makes sense for your game.
        }
    }

    public void TryRecruit()
    {
        if (recruitmentManager != null && !isRecruited)
        {
            recruitmentManager.TryRecruitNPC(this);
        }
    }

}

//}
/*
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{

    // public GameObject hintObject; // The game object to activate as a hint.
    //public string playerTag = "Player"; // The tag of the player object.
    private float activationDistance = 0.35f; // The distance at which the hint object should be activated.
    public NPCData npcData;
    private bool isNearNPC = false;
    private Transform player;

    public float followSpeed = 5f; // The speed at which the NPC follows the player

    private bool isFollowingPlayer = false;
    private bool standOnLeftSide = false;
    public float followDistance = 3f;
    private float distanceToPlayer;

    public float interactionRange = 2.0f; // The range at which the player can interact with the NPC
    public GameObject interactIcon;
    private RecruitmentManager recruitmentManager;
    private bool inRange = false;
    public bool isRecruited = false;

    void Start()
    {
        recruitmentManager = FindObjectOfType<RecruitmentManager>(); // Find the RecruitmentManager in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found in the scene");
        }
    }

    void Update()
    {
        if (player == null)
        {
            return; // Player not found; do nothing.
        }

        // Calculate the distance between the player and the NPC.
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && !isNearNPC)
        {
            // Player is within activation distance.
            inRange = true;
            // ActivateHintObject(true);
        }
        else if (distanceToPlayer > activationDistance && isNearNPC)
        {
            // Player moved away from the NPC.
            inRange = false;
            //ActivateHintObject(false);
        }
        if (inRange)
        {
            // Display an interaction icon or text above the NPC's head
            //interactIcon.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                TryRecruit();
            }
        }

        if (isFollowingPlayer)
        {
            // Calculate the direction from the NPC to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate the distance between the player and the NPC
            distanceToPlayer = directionToPlayer.magnitude;

            // Check if the NPC should stand on the left or right side based on player's facing direction
            standOnLeftSide = (player.localScale.x > 0);

            // Calculate the target position based on the player's facing direction
            Vector3 targetPosition = player.position + (standOnLeftSide ? -1 : 1) * Vector3.right * followDistance;

            // Move the NPC towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }



    public void Recruit()
    {
        isRecruited = true;
        isFollowingPlayer = true; // Enable following the player
    }

    public bool IsRecruited
    {
        get { return isRecruited; }
    }

    public Sprite GetRecruitedSprite()
    {
        // Assuming you have a SpriteRenderer component attached to the NPC
        SpriteRenderer npcSpriteRenderer = GetComponent<SpriteRenderer>();

        if (npcSpriteRenderer != null)
        {
            // Return the sprite from the NPC's sprite renderer
            return npcSpriteRenderer.sprite;
        }
        else
        {
            // Handle the case where the NPC doesn't have a SpriteRenderer component
            Debug.LogError("NPC does not have a SpriteRenderer component.");
            return null; // You can return a default sprite or handle it in a way that makes sense for your game.
        }
    }

    public string GetRecruitedName()
    {


        if (npcData != null)
        {
            // Return the sprite from the NPC's sprite renderer
            return npcData.npcName;
        }
        else
        {
            // Handle the case where the NPC doesn't have a SpriteRenderer component
            Debug.LogError("NPC does not have a NPCData attached to it.");
            return null; // You can return a default sprite or handle it in a way that makes sense for your game.
        }
    }

    public void TryRecruit()
    {
        if (recruitmentManager != null && !isRecruited)
        {
            recruitmentManager.TryRecruitNPC(this);
        }
    }
}
*/
//}

