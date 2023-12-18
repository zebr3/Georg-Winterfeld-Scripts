using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HLCPart : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Rigidbody2D rb;
    
    float speed = 2f;

    
    bool dragged = false;
    public string color;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragged = true;
         rb.velocity = Vector2.zero;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragged = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
       
        //only move when object ist not getting dragged
        if (!dragged)
        {
            rb.velocity = Vector3.down * speed;
        }
    }
}
