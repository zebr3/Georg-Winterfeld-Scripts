using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrganigrammSlots : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image image;
    public Transform parentBeforeDrag;
    [SerializeField] public NPCData npcData;
    [SerializeField] GameObject infoButton;
    

    //public delegate void ParentChangedEvent(Transform newParent);
    //public static event ParentChangedEvent OnParentChanged;

    private void Start()
    {
        parentBeforeDrag = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        infoButton.SetActive(false);
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        Transform newParent = transform.parent;
        infoButton.SetActive(true);
        infoButton.transform.position = gameObject.transform.position + new Vector3 (- 80, 80);

        /*
        if (newParent != parentBeforeDrag && OnParentChanged != null)
        {
            // Notify subscribers (InfoButtonsBewerbungsloader) that the parent has changed
            OnParentChanged.Invoke(newParent);
        } */

        transform.SetParent(parentBeforeDrag);
    }

}
