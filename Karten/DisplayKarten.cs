using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayKarten : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public NPCData npcData;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI weakAttackText;
    [SerializeField] Sprite[] typeIcons;
    [SerializeField] Image type;

    [SerializeField] Image image;

    [HideInInspector] public Transform parentbeforeDrag;
    
   
    public bool allowDrag = true;
    
   

    public void OnBeginDrag(PointerEventData eventData)
    {

        if(allowDrag)
        {
            GetComponent<Image>().raycastTarget = false;
            parentbeforeDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }




    }

    public void OnDrag(PointerEventData eventData)
    {

        if (allowDrag)
        {
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (allowDrag)
        {
            GetComponent<Image>().raycastTarget = true;
            transform.SetParent(parentbeforeDrag);
        }

        
        
           
        
    }

    void Start()
    {
        //displaying everything correctly
        nameText.text = npcData.npcName;
       if(npcData.profession == "Programmierer")
        {
            type.sprite = typeIcons[0];
        }
       else if(npcData.profession == "Artist")
        {
            type.sprite = typeIcons[1];
        }
        else if (npcData.profession == "Narrative")
        {
            type.sprite = typeIcons[2];
        }
        else if (npcData.profession == "Sound")
        {
            type.sprite = typeIcons[3];
        }
        else
        {
            type.sprite = typeIcons[4];
        }
        typeText.text = npcData.profession;
        attackText.text = npcData.attack.ToString();
        weakAttackText.text = npcData.weakAttack.ToString();
        
        image.sprite = npcData.npcSprite;
    }

  void Update()
    {
        attackText.text = npcData.attack.ToString();
        weakAttackText.text = npcData.weakAttack.ToString();

        if(transform.parent.GetComponent<Freizeit>() != null)
        {
            allowDrag = false;
        }
        else
        {
            allowDrag = true;
        }
    }

   
}
