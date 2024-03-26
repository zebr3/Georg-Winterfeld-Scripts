using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class NextRoundButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Kartenmanager kartenManager;
    Animator animator;
    bool allowAction;
    Vector3 originalScale;
    [SerializeField] bool isClicked = false;
    [SerializeField] GameObject optionsButton;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        optionsButton.transform.localScale = originalScale * 0.85f;
        isClicked = true;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        allowAction = true;
        if (!isClicked)
        {
            optionsButton.transform.localScale = originalScale * 0.9f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        allowAction = false;
        if (!isClicked)
        {
            optionsButton.transform.localScale = originalScale;
           
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        optionsButton.transform.localScale = originalScale;
        isClicked = false;
        if (allowAction)
        {
            optionsButton.GetComponent<Sanduhr>().animator.SetTrigger("StartAnimation");
            kartenManager.TurnIsOver();
        }
        
    }

    void Start()
    {
        animator = GetComponent<Animator>();
       originalScale = transform.localScale;
    }

    void EndAnimationMethod()
    {
        animator.SetTrigger("EndAnimation");
    }
}
