using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Freizeit : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] GameObject[] erschöpfungen;
    public TextMeshProUGUI text;
    [SerializeField] AudioSource freizeitSound;
   
    
    public void OnDrop(PointerEventData eventData)
    {
        
        GameObject droppedObject = eventData.pointerDrag.gameObject;
        DisplayKarten gedroppteKarte = droppedObject.GetComponent<DisplayKarten>();


        if (gedroppteKarte != null && transform.childCount < 2 && gedroppteKarte.npcData.profession != "Sonderkarte")
        {
            freizeitSound.Play();
            gedroppteKarte.parentbeforeDrag = transform;
            gedroppteKarte.transform.SetParent(transform);
            gedroppteKarte.transform.position = transform.position;
            text.text = transform.childCount.ToString() + "/2";
            gedroppteKarte.npcData.erschöpfung = 3;

            
            for(int i = 0; i < erschöpfungen.Length; i++)
            {
                erschöpfungen[i].GetComponent<Erschöpfung>().CheckErschöpfung();
            }
               

        }




        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {


        if (transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>() != null
            && transform.childCount < 2 && transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().npcData.profession != "Sonderkarte")
        {
            
            if (transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().allowShrink)
            {
                StartCoroutine(transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).GetComponent<DisplayKarten>().SmoothShrink());
            }

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

    void Start()
    {
        text.text = transform.childCount.ToString() + "/2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
