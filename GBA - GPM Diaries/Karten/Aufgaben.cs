using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Aufgaben : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI healthText;
    public TextMeshProUGUI counterText;
    [SerializeField] TextMeshProUGUI nameText;
    public AufgabenSO aufgabenSO;
    [SerializeField] Image image;
    [SerializeField] AudioSource taskKilledSound;
    [SerializeField] AudioSource oneDamage;
    [SerializeField] AudioSource threeDamage;
    [SerializeField] AudioSource moreThanFourDamage;
    [SerializeField] Animator sliceAnimation;
    [SerializeField] GameObject animationObject;
    [SerializeField] NPCData streitkarte;

    bool cardOverObject;


    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag.gameObject;
        DisplayKarten gedroppteKarte = droppedObject.GetComponent<DisplayKarten>();


        if (gedroppteKarte != null && gedroppteKarte.npcData.erschöpfung != 0)
        {

            //check the typing and attack with the right value
            if (gedroppteKarte.npcData.profession == aufgabenSO.profession || gedroppteKarte.npcData.profession == aufgabenSO.secondProfession)
            {
                aufgabenSO.health -= gedroppteKarte.npcData.attack;
                GameObject.Find("Linie").GetComponent<Linie>().StartAnnouncementAttack(gedroppteKarte.npcData.portrait);
                animationObject.SetActive(true);
                sliceAnimation.SetTrigger("StartSlice");


                if (gedroppteKarte.npcData.attack > 3)
                {

                    moreThanFourDamage.Play();

                    
                }
                else
                {

                    threeDamage.Play();
                }
            }
            else
            {
                aufgabenSO.health -= gedroppteKarte.npcData.weakAttack;
                GameObject.Find("Linie").GetComponent<Linie>().StartAnnouncementWeakAttack(gedroppteKarte.npcData.portrait);
                oneDamage.Play();
                animationObject.SetActive(true);
                sliceAnimation.SetTrigger("StartSlice");
            }

            //use ersch�pfung from card
            gedroppteKarte.npcData.erschöpfung--;

            for (int i = 0; i < 6; i++)
            {
                GameObject.Find("Erschöpfung").transform.GetChild(i).GetComponent<Erschöpfung>().CheckErschöpfung();
            }

            healthText.text = aufgabenSO.health.ToString();
            gedroppteKarte.parentbeforeDrag = GameObject.Find("Ablagestapel").transform;
            // gedroppteKarte.transform.SetParent(gedroppteKarte.parentbeforeDrag);





        }



        //what to do when health sf 0
        if (aufgabenSO.health <= 0)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().SoundWhenAufgabeIsDestroyed();

            if (transform.parent.gameObject.transform.childCount == 1)
            {
                StartCoroutine(GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().GameIsOverCoroutine());
            }
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

    public void OnPointerEnter(PointerEventData eventData)
    {


        if (transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>() != null)
        {

            if (transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().allowShrink && transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().npcData.profession != "Sonderkarte")
            {
                 StartCoroutine(transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().SmoothShrink());
                
            }

        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>() != null)
        {
            StopCoroutine(transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().SmoothShrink());
            transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().gameObject.transform.localScale = transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().originalScale;
            transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().allowShrink = true;


        }
    }


    public void EndSliceAnimation()
    {
        sliceAnimation.SetTrigger("EndSlice");
        animationObject.SetActive(false);
    }

    void Update()
    {
        if(aufgabenSO.countdown <= 0)
        {
            GameObject.Find("Kartenmanager").GetComponent<Kartenmanager>().ResetGame();
        }
       
    }
}
