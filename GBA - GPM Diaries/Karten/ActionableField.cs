using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionableField : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag.gameObject;


        if (droppedObject.GetComponent<DisplayKarten>().npcData.draw2 
            || droppedObject.GetComponent<DisplayKarten>().npcData.minus3NextTurn 
            || droppedObject.GetComponent<DisplayKarten>().npcData.plusOneAttack
            || droppedObject.GetComponent<DisplayKarten>().npcData.nothing
            )
        {
           
            droppedObject.GetComponent<DisplayKarten>().parentbeforeDrag = transform;
            droppedObject.GetComponent<DisplayKarten>().transform.position = transform.position;
            droppedObject.GetComponent<DisplayKarten>().transform.SetParent(transform);
        }

        if(transform.childCount > 1)
        {
          
            transform.GetChild(0).GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;
            transform.GetChild(0).GetComponent<DisplayKarten>().transform.SetParent(GameObject.Find("Ablagestapel").transform);
        }
        

        if (droppedObject.GetComponent<DisplayKarten>() != null && droppedObject.GetComponent<DisplayKarten>().npcData.draw2)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().DrawTwoCards();
            //droppedObject.GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;
         
           

        }
        else if (droppedObject.GetComponent<DisplayKarten>() != null && droppedObject.GetComponent<DisplayKarten>().npcData.minus3NextTurn)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().StreitkarteMinus3NextTurn();
            //GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().teamData.specialCards.Remove(droppedObject.GetComponent<DisplayKarten>().npcData);
           // Destroy(droppedObject);
            

        }
        else if (droppedObject.GetComponent<DisplayKarten>() != null && droppedObject.GetComponent<DisplayKarten>().npcData.plusOneAttack)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().PlusOneAttack();
            //droppedObject.GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;

        }
        else if (droppedObject.GetComponent<DisplayKarten>() != null && droppedObject.GetComponent<DisplayKarten>().npcData.nothing)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().NothingCard();
           // droppedObject.GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;

        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       

        if (transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>() != null
            && transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().npcData.plusOneAttack
            || transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().npcData.draw2
            || transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().npcData.minus3NextTurn
            || transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().npcData.nothing)
        {

            if (transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().allowShrink)
            {
                StartCoroutine(transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().SmoothShrink());
            }

        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>() != null)
        {
            transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().gameObject.transform.localScale = transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().originalScale;
            transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<DisplayKarten>().allowShrink = true;
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
