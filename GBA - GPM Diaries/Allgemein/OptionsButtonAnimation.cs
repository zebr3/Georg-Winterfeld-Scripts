using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class OptionsButtonAnimation : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] bool isClicked = false;
    [SerializeField] bool allowAction;
    [SerializeField] GameObject raycastBlocker;
    [SerializeField] GameObject optionsButton;
    Vector3 originalScale;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (raycastBlocker.activeSelf == false)
        {
            optionsButton.transform.localScale = originalScale * 0.85f;
            isClicked = true;
        }
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (raycastBlocker.activeSelf == false)
        {
            if (!isClicked)
            {
                optionsButton.transform.localScale = originalScale * 0.9f;
            }
        }
        
       // allowAction = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(raycastBlocker.activeSelf == false) 
        {
            if (!isClicked)
            {
                optionsButton.transform.localScale = originalScale;
            }

        }

        //allowAction = false;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(raycastBlocker.activeSelf == false)
        {
            optionsButton.transform.localScale = originalScale;
            isClicked = false;
            if (allowAction)
            {
                //   PerformAction();
            }
        }
        
    }

    public void PerformAction()
    {
       // GameObject.Find("SceneTransition").GetComponent<LevelTransition>().FadedLoadScene("1_0Tutor");
    }

    void Start()
    {
        originalScale = optionsButton.transform.localScale;
    }
}
