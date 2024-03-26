using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LinkHandlerForClick : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    private TMP_Text _tmpTextbox;
    [SerializeField] Canvas _canvasToCheck;
    [SerializeField] private Camera _camera;
   

    public delegate void ClickOnLinkEvent(string keyword);
    public static event ClickOnLinkEvent OnClickedOnLinkEvent;

    private void Awake()
    {
        _tmpTextbox = GetComponent<TMP_Text>();
        _canvasToCheck = GetComponentInParent<Canvas>();

        if (_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            _camera = null;
        }
        else
            _camera = _canvasToCheck.worldCamera;

        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);
        _tmpTextbox.GetComponent<TMP_Text>().overrideColorTags = true;
        _tmpTextbox.GetComponent<TMP_Text>().color = new Color32(255, 0, 0, 255);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);
        _tmpTextbox.GetComponent<TMP_Text>().overrideColorTags = true;
        _tmpTextbox.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);

        var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(_tmpTextbox, mousePosition, _camera);

        if (linkTaggedText == -1) return;
       
        TMP_LinkInfo linkInfo = _tmpTextbox.textInfo.linkInfo[linkTaggedText];

        string linkID =  linkInfo.GetLinkID();
        if (linkID.Contains("https://"))
        {
            Application.OpenURL(linkID);
            return;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if( OnClickedOnLinkEvent != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
