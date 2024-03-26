using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawTwo : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag.gameObject;

        if(droppedObject.GetComponent<DisplayKarten>() != null && droppedObject.GetComponent<DisplayKarten>().npcData.draw2)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().DrawTwoCards();
            droppedObject.GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;
           
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
