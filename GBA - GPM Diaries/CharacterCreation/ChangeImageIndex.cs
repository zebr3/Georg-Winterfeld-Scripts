using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeImageIndex : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int direction;
    ChangeImageSelection changeImageSelection;
    [SerializeField] AudioSource clickSound;
    bool allowAction;

    Vector3 originalScale;
    bool isClicked;
    [SerializeField] GameObject button;

   

    public void OnPointerDown(PointerEventData eventData)
    {
        
        button.transform.localScale = originalScale * 0.9f;
        isClicked = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        allowAction = true;
        if(!isClicked)
        {
            button.transform.localScale = originalScale * 0.96f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        allowAction = false;
        if(!isClicked)
        {
            button.transform.localScale = originalScale;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        button.transform.localScale = originalScale;
        isClicked = false;

        if (allowAction)
        {
            clickSound.Play();
            changeImageSelection.direction = direction;
            changeImageSelection.UpdateSelection();
        }
       
        

    }

    // Start is called before the first frame update
    void Start()
    {
        changeImageSelection = GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>();
        originalScale = transform.localScale;
    }

    
}
