using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayKarten : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public NPCData npcData;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI weakAttackText;
    [SerializeField] Sprite[] typeIcons;
    [SerializeField] Image type;
    [SerializeField] GameObject normalRahmen;
    [SerializeField] GameObject spezialRahmen;
    [SerializeField] GameObject attackIcon;
    [SerializeField] GameObject weakAttackIcon;
    [SerializeField] TextMeshProUGUI description;
    int positionInHand;
   

    [SerializeField] Image image;

     public Transform parentbeforeDrag;

    public Vector2 originalScale;
    Vector2 targetScale;


    public bool allowDrag = true;
    public bool allowShrink = true;



    public void OnBeginDrag(PointerEventData eventData)
    {

        if (allowDrag)
        {
            positionInHand = transform.GetSiblingIndex();
            GetComponent<Image>().raycastTarget = false;
            parentbeforeDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

            //save data for shrinking and start shrinking
            allowShrink = true;
            originalScale = transform.localScale;

            targetScale = originalScale * 0.5f;

           

            

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
            if (parentbeforeDrag == GameObject.Find("Hand").transform)
            {
                transform.SetSiblingIndex(positionInHand);
            }
        }

        //restore scale if card has been changed
        transform.localScale = originalScale;



    }

   public IEnumerator waitForShrinkAgain()
    {
        yield return new WaitForSeconds(0.2f);
        allowShrink = true;
    }


    public IEnumerator SmoothShrink()
    {
        allowShrink = false;

        float duration = 0.1f; 
        float elapsedTime = 0f;

        

        while (elapsedTime < duration)
        {
            transform.localScale = Vector2.Lerp(originalScale, targetScale, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //transform.localScale = targetScale; 
        
        
    }

    void Start()
    {
        //if special card, turn the specific frame on
        if(npcData.profession == "Sonderkarte")
        {
            spezialRahmen.SetActive(true);
            normalRahmen.SetActive(false);
            attackText.enabled = false;
            weakAttackText.enabled = false;
            attackIcon.SetActive(false);
            weakAttackIcon.SetActive(false);
            description.gameObject.SetActive(true);
            description.text = npcData.description;

        }

        //displaying everything correctly
        nameText.text = npcData.npcName;
        if (npcData.profession == "Programmer")
        {
            type.sprite = typeIcons[0];
        }
        else if (npcData.profession == "Artist")
        {
            type.sprite = typeIcons[1];
        }
        else if (npcData.profession == "Game Writing")
        {
            type.sprite = typeIcons[2];
        }
        else if (npcData.profession == "Sound")
        {
            type.sprite = typeIcons[3];
        }
        else if (npcData.profession == "Game Design")
        {
            type.sprite = typeIcons[4];
        }
        else
        {
            type.sprite = typeIcons[5];
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

        if (transform.parent.GetComponent<Freizeit>() != null || parentbeforeDrag == GameObject.Find("ActionableField").transform)
        {
            allowDrag = false;
        }
        else
        {
            allowDrag = true;
        }

       if(transform.localScale == (Vector3)originalScale)
        {
            
            //allowShrink = true;
        }

      

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
}
