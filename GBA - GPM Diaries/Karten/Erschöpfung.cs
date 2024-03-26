using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Erschöpfung : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] NPCData npcdata;
    [SerializeField] GameObject green;
    [SerializeField] GameObject yellow;
    [SerializeField] GameObject red;
    [SerializeField] GameObject empty;

    [SerializeField] string assignedProfession;
    void Start()
    {
       // GetNPCDataToErschöpfung();
        CheckErschöpfung();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //gets the correct characters data to show correct erschöpfung
    public void GetNPCDataToErschöpfung()
    {
        Kartenmanager kartenmanager = GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>();

        for(int i = 0; i < kartenmanager.cardsInDeck.Count; i++)
        {
            if(kartenmanager.cardsInDeck[i].profession == assignedProfession)
            {
                npcdata = kartenmanager.cardsInDeck[i];

                if(kartenmanager.cardsInDeck[i].profession == "Artist2")
                {
                    Debug.Log("zweiten gefunden");
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

    public void OnPointerEnter(PointerEventData eventData)
    {
       

        if (transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>() != null 
            && transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().npcData.erschöpfungRegen
            && transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().npcData.erschöpfung != 3)
        {

            if (transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().allowShrink)
            {
                StartCoroutine(transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().SmoothShrink());
            }

        }

    }
    IEnumerator animationstuff()
    {
        Vector3 originalScale = new Vector3(1, 1, 1);
        Vector3 targetScale = originalScale * 2;

        float duration = 0.5f; // Adjust as needed
        float elapsedTime = 0f;



        while (elapsedTime < duration)
        {
            green.transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);
            yellow.transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);
            red.transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);
            empty.transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
           
        }

        targetScale = originalScale;
         duration = 0.1f; // Adjust as needed
         elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            green.transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);
            yellow.transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);
            red.transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);
            empty.transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>() != null)
        {
            transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().gameObject.transform.localScale = transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().originalScale;
            transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().allowShrink = true;
        }
    }
}
