using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HLCDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] string color;
    
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedGameObject = eventData.pointerDrag.gameObject;
        if(droppedGameObject.GetComponent<HLCPart>().color == color)
        {
            gameObject.SetActive(false);
            transform.parent.parent.GetComponent<HLC>().correctParts += 1;
            Destroy(droppedGameObject);
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
