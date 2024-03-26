using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject hLCBeginning;
    [SerializeField] GameObject minigameManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        hLCBeginning.SetActive(false);
        minigameManager.SetActive(true);

       
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
