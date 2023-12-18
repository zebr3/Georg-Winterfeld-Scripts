using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Freizeit : MonoBehaviour, IDropHandler
{

    [SerializeField] GameObject[] erschöpfungen;
    
    public void OnDrop(PointerEventData eventData)
    {
        
        GameObject droppedObject = eventData.pointerDrag.gameObject;
        DisplayKarten gedroppteKarte = droppedObject.GetComponent<DisplayKarten>();


        if (gedroppteKarte != null && transform.childCount == 0 && gedroppteKarte.npcData.profession != "Sonderkarte")
        {
           
            gedroppteKarte.parentbeforeDrag = transform;
            gedroppteKarte.transform.SetParent(transform);
           // gedroppteKarte.allowDrag = false;
            gedroppteKarte.npcData.erschöpfung = 3;
            
            for(int i = 0; i < erschöpfungen.Length; i++)
            {
                erschöpfungen[i].GetComponent<Erschöpfung>().CheckErschöpfung();
            }
               

        }




        
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
