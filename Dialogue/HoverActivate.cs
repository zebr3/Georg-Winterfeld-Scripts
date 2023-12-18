using UnityEngine;
using UnityEngine.EventSystems;

public class HoverActivate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToActivate;

    private void Start()
    {
        // Ensure the object to activate is initially deactivated
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    // Called when the mouse pointer enters the object's collider
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered!");

        // Activate the specified object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }

    // Called when the mouse pointer exits the object's collider
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited!");

        // Deactivate the specified object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }
}
