using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite pressedImage;
    Sprite normalImage;
    bool allowAction;
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = pressedImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        allowAction = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        allowAction = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = normalImage;

        if (allowAction)
        {
            GameObject.Find("SceneTransition").GetComponent<LevelTransition>().FadedLoadScene("2_3Konflikt1");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        normalImage = GetComponent<Image>().sprite;
    }

  
}
