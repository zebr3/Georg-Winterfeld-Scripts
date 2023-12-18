using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SimpleDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] TextMeshProUGUI speakerName;
    [SerializeField] Button continueButton;
    [SerializeField] Image portraitImage;

    [Header("Dialogue")]
    [TextArea]
    [SerializeField] string[] lines;
    [SerializeField] string[] speakerSet;
    [SerializeField] Sprite[] portrait;
    [SerializeField] float textSpeed;

    private int index;
    [SerializeField] RecruitmentManager recruitmentManager;
    [SerializeField] bool setDialogueCharacters;
    [Header("Scene Transition")]
    public bool changeScene;
    public string sceneName;
    public LevelTransition levelTransition;

    // Start is called before the first frame update
    void Start()
    {
      
        textComponent.text = string.Empty;
        continueButton.gameObject.SetActive(false);
        StartDialogue();
        recruitmentManager = GameObject.Find("RecruitmentManager").GetComponent<RecruitmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button to skip the current line
        {
            CompleteLine();
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        string line = lines[index];
        SetTextOpacity(0f);

        if (setDialogueCharacters)
        {
            speakerName.text = speakerSet[index];
            portraitImage.sprite = portrait[index];
        }
        else
        {
            SetSpeaker(speakerSet[index]);
        }

        // Replace placeholders with NPC names based on their professions
        foreach (NPCData npcData in recruitmentManager.recruitedNPCData)
        {
            string placeholder = "{" + npcData.profession + "}";
            line = line.Replace(placeholder, npcData.npcName);
        }

        textComponent.text = line;

        yield return new WaitForSeconds(textSpeed);

        int totalCharacters = line.Length;
        int visibleCharacters = 0;
        SetTextOpacity(100f);

        while (visibleCharacters <= totalCharacters)
        {
            textComponent.maxVisibleCharacters = visibleCharacters;
            visibleCharacters++;
            yield return new WaitForSeconds(textSpeed);
        }

        continueButton.gameObject.SetActive(true);
    }

    private void SetSpeaker(string speaker)
    {
        foreach (NPCData npcData in recruitmentManager.recruitedNPCData)
        {
            if (npcData.profession.Equals(speaker, StringComparison.OrdinalIgnoreCase))
            {
                speakerName.text = npcData.npcName;
                portraitImage.sprite = npcData.portrait;
                return;
            }
            else
            {
                speakerName.text = speakerSet[index];
                portraitImage.sprite = portrait[index];
            }
        }
    }

    private void SetTextOpacity(float alpha)
    {
        Color currentColor = textComponent.color;
        currentColor.a = alpha;
        textComponent.color = currentColor;
    }

    public void ContinueButtonPressed()
    {
        continueButton.gameObject.SetActive(false);

        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);

            if (changeScene)
            {
                if (!string.IsNullOrEmpty(sceneName))
                {
                    ChangeScene(sceneName);
                }
                else
                {
                    Debug.LogWarning("Scene name is not set for scene change.");
                }
            }
        }
    }

    void CompleteLine()
    {
        StopAllCoroutines();
        textComponent.maxVisibleCharacters = lines[index].Length;
        continueButton.gameObject.SetActive(true);
    }

    void ChangeScene(string sceneName)
    {
        levelTransition.FadedLoadScene(sceneName);
    }
}
