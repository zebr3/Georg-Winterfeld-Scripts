using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipHandlerHover : MonoBehaviour
{
    [SerializeField] private List<TooltipInfos> tooltipContentList;
    [SerializeField] private GameObject tooltipContainer;
    private TMP_Text tooltipDescriptionTMP;

    private void Awake()
    {
        tooltipDescriptionTMP = tooltipContainer.GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        LinkHandlerOnHover.OnHoverOnLinkEvent += GetTooltipInfo;
        LinkHandlerOnHover.OnCloseTooltipEvent += CloseTooltip;
    }

    private void OnDisable()
    {
        LinkHandlerOnHover.OnHoverOnLinkEvent -= GetTooltipInfo;
        LinkHandlerOnHover.OnCloseTooltipEvent -= CloseTooltip;
    }

    private void GetTooltipInfo(string keyword, Vector3 mousePos)
    {
        foreach (var entry in tooltipContentList) 
        { 
            if(entry.Keyword  == keyword)
            {
                if (!tooltipContainer.gameObject.activeInHierarchy)
                {
                    tooltipContainer.transform.position = mousePos + new Vector3(0, 500, 0);
                    tooltipContainer.gameObject.SetActive(true);
                }

                tooltipDescriptionTMP.text = entry.Description;
                    return;
            }
        }

        Debug.Log(message: $"Keyword: {keyword} not found");
    }

    public void CloseTooltip() 
    { 
        if(tooltipContainer.gameObject.activeInHierarchy)
            tooltipContainer.gameObject.SetActive(false);
    
    
    }
}
