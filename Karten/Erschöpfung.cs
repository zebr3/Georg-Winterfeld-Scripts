using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Erschöpfung : MonoBehaviour, IDropHandler
{
    [SerializeField] NPCData npcdata;
    [SerializeField] GameObject green;
    [SerializeField] GameObject yellow;
    [SerializeField] GameObject red;
    [SerializeField] GameObject empty;

    [SerializeField] string assignedProfession;
    void Start()
    {
        GetNPCDataToErschöpfung();
        CheckErschöpfung();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //gets the right characters data 
    void GetNPCDataToErschöpfung()
    {
        Kartenmanager kartenmanager = GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>();

        for(int i = 0; i < kartenmanager.cardsInDeck.Count; i++)
        {
            if(kartenmanager.cardsInDeck[i].profession == assignedProfession)
            {
                npcdata = kartenmanager.cardsInDeck[i];

                if(kartenmanager.cardsInDeck[i].profession == "Artist2")
                {
                    kartenmanager.cardsInDeck[i].profession = "Artist";
                }
            }
        }
    }

    //uses this method when card is played, checks the status of erschöpfung and assigns the right value
    public void CheckErschöpfung()
    {
       
        if(npcdata.erschöpfung == 3)
        {
            green.gameObject.SetActive(true);
            yellow.gameObject.SetActive(false);
            red.gameObject.SetActive(false);
            empty.gameObject.SetActive(false);
        }
        else if(npcdata.erschöpfung == 2)
        {
            green.gameObject.SetActive(false);
            yellow.gameObject.SetActive(true);
            red.gameObject.SetActive(false);
            empty.gameObject.SetActive(false);
        }
        else if (npcdata.erschöpfung == 1)
        {
            green.gameObject.SetActive(false);
            yellow.gameObject.SetActive(false);
            red.gameObject.SetActive(true);
            empty.gameObject.SetActive(false);
        }
        else if (npcdata.erschöpfung == 0)
        {
            green.gameObject.SetActive(false);
            yellow.gameObject.SetActive(false);
            red.gameObject.SetActive(false);
            empty.gameObject.SetActive(true);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag.gameObject;

        if(droppedObject.GetComponent<DisplayKarten>().npcData.erschöpfungRegen && npcdata.erschöpfung != 3)
        {
            npcdata.erschöpfung += 1;
            CheckErschöpfung();
            droppedObject.GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;

        }
    }
}
