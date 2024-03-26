using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ButtonClickAnimationPlay : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] bool isClicked = false;
    [SerializeField] bool allowAction;
    [SerializeField] GameObject button;
    [SerializeField] RecruitedNpcScriptableObject team;
    [SerializeField] NPCData[] specialCards;
    Vector3 originalScale;
    [SerializeField] Color normalColor = new Color(255, 255, 255, 255);
    [SerializeField] Color hoverColor = new Color(229, 229, 229, 255);
    [SerializeField] Color clickColor = new Color(200, 200, 200, 255);

    public void OnPointerDown(PointerEventData eventData)
    {
        button.transform.localScale = originalScale * 0.9f;
        button.GetComponent<Image>().color = clickColor;
        isClicked = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            button.transform.localScale = originalScale * 0.96f;
            button.GetComponent<Image>().color = hoverColor;
        }
        allowAction = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        allowAction = false;
        if (!isClicked)
        {
            button.transform.localScale = originalScale;
            button.GetComponent<Image>().color = normalColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        button.transform.localScale = originalScale;
        isClicked = false;
        button.GetComponent<Image>().color = normalColor;
        if (allowAction)
        {
            PerformAction();

        }
    }

    public void PerformAction()
    {
        GameObject.Find("SceneTransition").GetComponent<LevelTransition>().FadedLoadScene("1_0Tutor");
        team.team.Clear();
        team.specialCards.Clear();
        for(int i = 0; i < specialCards.Length; i++)
        {
            team.specialCards.Add(specialCards[i]);
        }


    }

    void Start()
    {
        originalScale = button.transform.localScale;
    }
}
