using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeImageIndex : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int direction;
    ChangeImageSelection changeImageSelection;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        changeImageSelection.direction = direction;
        changeImageSelection.UpdateSelection();
    }

    // Start is called before the first frame update
    void Start()
    {
        changeImageSelection = GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
