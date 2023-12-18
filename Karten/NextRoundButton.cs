using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextRoundButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Kartenmanager kartenManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        kartenManager.TurnIsOver();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
