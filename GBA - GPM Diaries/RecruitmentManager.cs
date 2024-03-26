using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections;

public class RecruitmentManager : MonoBehaviour
{
    public bool displayUI;
    public bool dontDestroyOnLoad;
    public bool loadTeam;
    public bool resetProfessions;
    public bool resetTeam;
    public GameObject recruitmentUI;
    public Transform[] npcImageContainers;
    public Transform[] npcNameContainers;
    public float recruitmentTimeLimit = 90.0f;
    public int maxRecruitSlots = 5;
    public List<NPCData> allNPCData;
    public GameObject UIPanel;
    public PlayerController playerController;
    public GameObject continuePanel;

    private float remainingRecruitmentTime;
    public List<NPCData> recruitedNPCData = new List<NPCData>();
    public List<NPCData> maybeStack = new List<NPCData>();
    private bool isRecruitmentActive = true;
    private bool continuePanelActivated = false;

   

    [SerializeField] public RecruitedNpcScriptableObject teamData;



    void Start()
    {
       

      if (resetTeam == true)
        {
            teamData.team.Clear();
        }
       

        if (displayUI == true)
        {
            
            // Check if UI elements are assigned when displayUI is true
            if (recruitmentUI == null || npcImageContainers.Any(container => container == null) ||
                npcNameContainers.Any(container => container == null) || UIPanel == null ||
                playerController == null || continuePanel == null)
            {
                //Debug.LogError("UI elements are not assigned in the inspector.");
                return; // Return to prevent further execution
            }
        }

        remainingRecruitmentTime = recruitmentTimeLimit;

        if (displayUI && recruitmentUI != null)
        {
            recruitmentUI.SetActive(true);
            UpdateRecruitmentUI();
            //ClearNPCImages();
        }

    }

    void Awake()
    {
        if (loadTeam == true)
        {
            recruitedNPCData.AddRange(teamData.team);
        }


        if (resetProfessions == true)
        {
            foreach (NPCData npcData in allNPCData)
            {
                npcData.profession = npcData.standardProfession;
            }
        }
        // Check if an instance of the RecruitmentManager already exists
        if (FindObjectsOfType<RecruitmentManager>().Length > 1)
        {
            // Destroy the duplicate instance
            Destroy(gameObject);
        }
        else if(dontDestroyOnLoad == true)
        {
            // Make this instance persistent between scenes
            DontDestroyOnLoad(this);
        }
    }

    void Update()
    {
        if (isRecruitmentActive)
        {
            remainingRecruitmentTime -= Time.deltaTime;

            if (remainingRecruitmentTime <= 0)
            {
                StartCoroutine(EndRecruitment());
            }
        }
       

        // Check if recruitedNPCData count is 5 and the continuePanel hasn't been activated
        if (displayUI && continuePanel != null && recruitedNPCData.Count == 5 && !continuePanelActivated)
        {
            StartCoroutine (ActivateContinuePanelDelayed());
        }

    }

    private IEnumerator ActivateContinuePanelDelayed()
    {
        // Check if recruitedNPCData count is 5 and the continuePanel hasn't been activated
        if (displayUI && continuePanel != null && recruitedNPCData.Count == 5 && !continuePanelActivated)
        {
            // Wait for 3 seconds
            yield return new WaitForSeconds(1);

            // Activate the continuePanel
            continuePanel.SetActive(true);

            // Set continuePanelActivated to true
            continuePanelActivated = true;
        }
    }

    public void TryRecruitNPC(NPCController npc)
    {
        if (npc != null && !npc.IsRecruited && isRecruitmentActive)
        {
            if (recruitedNPCData.Count < maxRecruitSlots)
            {
                npc.Recruit();
                Image imageContainer = npcImageContainers[recruitedNPCData.Count].GetComponentInChildren<Image>();
                imageContainer.sprite = npc.GetRecruitedSprite();

                TextMeshProUGUI npcName = npcNameContainers[recruitedNPCData.Count].GetComponentInChildren<TextMeshProUGUI>();
                npcName.text = npc.GetRecruitedName();

                recruitedNPCData.Add(npc.npcData);
                teamData.team.Add(npc.npcData);

                if (recruitedNPCData.Count >= maxRecruitSlots)
                {
                    StartCoroutine(EndRecruitment());
                }
            }
            else
            {
                StartCoroutine(EndRecruitment());
            }
        }
    }

    public void RecruitNPC(NPCData npcData)
    {
        if (npcData != null && !npcData.IsRecruited && isRecruitmentActive)
        {
            if (recruitedNPCData.Count < maxRecruitSlots)
            {
                Image imageContainer = npcImageContainers[recruitedNPCData.Count].GetComponentInChildren<Image>();
                imageContainer.sprite = npcData.portrait;

                TextMeshProUGUI npcName = npcNameContainers[recruitedNPCData.Count].GetComponentInChildren<TextMeshProUGUI>();
                npcName.text = npcData.npcName;

                recruitedNPCData.Add(npcData);
                teamData.team.Add(npcData);

                if (recruitedNPCData.Count >= maxRecruitSlots)
                {
                    StartCoroutine(EndRecruitment());
                }
            }
            else
            {
                StartCoroutine(EndRecruitment());
            }
        }
    }

    public void RecruitNPCFromMaybeStack(NPCData npcData)
    {
        if (npcData != null && !npcData.IsRecruited && isRecruitmentActive)
        {
            if (recruitedNPCData.Count < maxRecruitSlots)
            {
                Image imageContainer = npcImageContainers[recruitedNPCData.Count].GetComponentInChildren<Image>();
                imageContainer.sprite = npcData.portrait;

                TextMeshProUGUI npcName = npcNameContainers[recruitedNPCData.Count].GetComponentInChildren<TextMeshProUGUI>();
                npcName.text = npcData.npcName;

                recruitedNPCData.Add(npcData);
                teamData.team.Add(npcData);

                if (recruitedNPCData.Count >= maxRecruitSlots)
                {
                    StartCoroutine(EndRecruitment());
                }
            }
            else
            {
                StartCoroutine(EndRecruitment());
            }
        }
    }


    public void MaybeStackNPC(NPCData npcData)
    {
        if (npcData != null && !npcData.IsRecruited && isRecruitmentActive)
        {
            if (recruitedNPCData.Count < maxRecruitSlots)
            {
                maybeStack.Add(npcData);

                if (recruitedNPCData.Count >= maxRecruitSlots)
                {
                    StartCoroutine(EndRecruitment());
                }
            }
            else
            {
                StartCoroutine(EndRecruitment());
            }
        }
    }

    private IEnumerator EndRecruitment()
    {
        isRecruitmentActive = false;

        if (recruitedNPCData.Count < maxRecruitSlots)
        {
            var unrecruitedNPCData = allNPCData.Except(recruitedNPCData).ToList();

            for (int i = recruitedNPCData.Count; i < maxRecruitSlots; i++)
            {
                if (unrecruitedNPCData.Count > 0)
                {
                    int randomIndex = Random.Range(0, unrecruitedNPCData.Count);
                    NPCData randomNPCData = unrecruitedNPCData[randomIndex];
                    recruitedNPCData.Add(randomNPCData);
                    unrecruitedNPCData.RemoveAt(randomIndex);

                    Image imageContainer = npcImageContainers[i].GetComponentInChildren<Image>();
                    imageContainer.sprite = randomNPCData.npcSprite;

                    TextMeshProUGUI npcName = npcNameContainers[i].GetComponentInChildren<TextMeshProUGUI>();
                    npcName.text = randomNPCData.npcName;
                }
            }
        }

        yield return new WaitForSeconds(1.0f);

        UIPanel.SetActive(true);
    }

    private void UpdateRecruitmentUI()
    {
        if (displayUI)
        {
            // Check if UI elements are assigned when displayUI is true
            if (recruitmentUI == null || npcImageContainers.Any(container => container == null) ||
                npcNameContainers.Any(container => container == null))
            {
                Debug.LogError("UI elements are not assigned in the inspector.");
                return; // Return to prevent further execution
            }
        }

        // Implement UI updates here
    }

    /*
    private void ClearNPCImages()
    {
        foreach (Transform container in npcImageContainers)
        {
            Image imageContainer = container.GetComponentInChildren<Image>();
            imageContainer.sprite = null;
        }
    } */
}
