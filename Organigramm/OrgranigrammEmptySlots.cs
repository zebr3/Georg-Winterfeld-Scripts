using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class OrgranigrammEmptySlots : MonoBehaviour, IDropHandler
{
    bool once = true;
    [SerializeField] string profession;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject droppedObject = eventData.pointerDrag.gameObject;
            droppedObject.GetComponent<OrganigrammSlots>().parentBeforeDrag = transform;
            droppedObject.GetComponent<OrganigrammSlots>().npcData.profession = profession;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 1 && once)
        {
            GameObject.FindWithTag("Organigramm").GetComponent<Organigramm>().slotsAssigned += 1;
            once = false;
        }
    }
}
