using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClickTooltipHandler : MonoBehaviour
{
    [SerializeField] private List<TooltipInfos> tooltipContentList;
    [SerializeField] private GameObject tooltipContainer;
    private TMP_Text _tooltipDescriptionTMP;
    //[SerializeField] private Image iconDisplay;

    private void Awake()
    {
        _tooltipDescriptionTMP = tooltipContainer.GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        LinkHandlerForClick.OnClickedOnLinkEvent += GetTooltipInfo;
    }

    private void OnDisable()
    {
        LinkHandlerForClick.OnClickedOnLinkEvent -= GetTooltipInfo;
    }

    private void GetTooltipInfo(string keyword)
    {
        if (Input.GetMouseButtonDown(1))  // Check if right mouse button is clicked
        {
            foreach (var entry in tooltipContentList)
            {
                if (entry.Keyword == keyword)
                {
                    if (!tooltipContainer.gameObject.activeInHierarchy)
                    {
                        tooltipContainer.gameObject.SetActive(true);
                    }

                    _tooltipDescriptionTMP.text = entry.Keyword;
                    //iconDisplay.sprite = entry.Image
                    return;
                }
            }

            Debug.Log(message: $"Keyword: {keyword} not found");
        }
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
