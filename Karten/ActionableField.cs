using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionableField : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag.gameObject;

        if (droppedObject.GetComponent<DisplayKarten>() != null && droppedObject.GetComponent<DisplayKarten>().npcData.draw2)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().DrawTwoCards();
            droppedObject.GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;

        }
        else if (droppedObject.GetComponent<DisplayKarten>() != null && droppedObject.GetComponent<DisplayKarten>().npcData.minus3NextTurn)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().StreitkarteMinus3NextTurn();
            droppedObject.GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;

        }
        else if (droppedObject.GetComponent<DisplayKarten>() != null && droppedObject.GetComponent<DisplayKarten>().npcData.plusOneAttack)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().PlusOneAttack();
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
