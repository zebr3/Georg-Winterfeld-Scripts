using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TMP_Text))]
public class LinkHandlerOnHover : MonoBehaviour
{
    private TMP_Text tmpTextBox;
    [SerializeField] private Canvas canvasToCheck;
    [SerializeField] private Camera cameraToUse;
    private RectTransform textBoxRectTransform;

    private int currentlyActiveLinkedElement;

    public delegate void HoverOnLinkEvent(string keyword, Vector3 mousePos);
    public static event HoverOnLinkEvent OnHoverOnLinkEvent;

    public delegate void CloseTooltipEvent();
    public static event CloseTooltipEvent OnCloseTooltipEvent;

    private void Awake()
    {
        tmpTextBox = GetComponent<TMP_Text>();
       
        textBoxRectTransform = GetComponent<RectTransform>();

        if (canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
            cameraToUse = null;
        else
            cameraToUse = canvasToCheck.worldCamera;
    }

    
    // Update is called once per frame
    void Update()
    {
        CheckForLinkAtMousePosition();
    }

    private void CheckForLinkAtMousePosition()
    {
        Vector3 mousePosition = (Vector3)Mouse.current.position.ReadValue();

        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(textBoxRectTransform, mousePosition, cameraToUse);

        if (!isIntersectingRectTransform)
            return;

        int intersectingLink = TMP_TextUtilities.FindIntersectingLink(tmpTextBox, mousePosition, cameraToUse);

        if (currentlyActiveLinkedElement != intersectingLink)
            OnCloseTooltipEvent?.Invoke();

        if (intersectingLink == -1)
            return;

        TMP_LinkInfo linkInfo = tmpTextBox.textInfo.linkInfo[intersectingLink];

        OnHoverOnLinkEvent?.Invoke(keyword: linkInfo.GetLinkID(), mousePosition);
        currentlyActiveLinkedElement = intersectingLink;
    }
}
