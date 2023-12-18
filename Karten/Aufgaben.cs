using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Aufgaben : MonoBehaviour, IDropHandler
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] TextMeshProUGUI nameText;
    public AufgabenSO aufgabenSO;
    [SerializeField] Image image;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag.gameObject;
        DisplayKarten gedroppteKarte = droppedObject.GetComponent<DisplayKarten>();
        

        if (gedroppteKarte != null && gedroppteKarte.npcData.erschöpfung != 0)
        {
            
            //check the typing and attack with the right value
            if(gedroppteKarte.npcData.profession == aufgabenSO.profession)
            {
                aufgabenSO.health -= gedroppteKarte.npcData.attack;
            }
            else
            {
                aufgabenSO.health -= gedroppteKarte.npcData.weakAttack;
            }

            //use erschöpfung from card
            gedroppteKarte.npcData.erschöpfung--;

            for (int i = 0; i < 6; i++)
            {
                GameObject.Find("Erschöpfung").transform.GetChild(i).GetComponent<Erschöpfung>().CheckErschöpfung();
            }

            healthText.text = aufgabenSO.health.ToString();
            gedroppteKarte.parentbeforeDrag = GameObject.Find("Ablagestapel").transform;
           // gedroppteKarte.transform.SetParent(gedroppteKarte.parentbeforeDrag);
            
          

            

        }

        

        //what to do when health if 0
        if (aufgabenSO.health <= 0)
        {
            Destroy(gameObject);
            
        }

       
    }

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = aufgabenSO.health.ToString();
        counterText.text = aufgabenSO.countdown.ToString();
        nameText.text = aufgabenSO.name;
        image.sprite = aufgabenSO.sprite;
    }
}
