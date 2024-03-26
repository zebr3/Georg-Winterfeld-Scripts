using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System;

public class TooltipHandlerHover : MonoBehaviour
{
    [SerializeField] public List<TooltipInfos> tooltipContentList;
    [SerializeField] public GameObject tooltipContainer;
    [SerializeField] TMP_Text tooltipTitleTMP;
    [SerializeField] TMP_Text tooltipDescriptionTMP;
    [SerializeField] public WikiMemory wikiMemory;
    [SerializeField] bool resetWikiMemory;

    public SimpleDialogue simpleDialogue; // Reference to SimpleDialogue script

    private void Awake()
    {
        //tooltipDescriptionTMP = tooltipContainer.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        if (wikiMemory != null && resetWikiMemory == true)
        {
            Debug.Log("Wiki Reset!");
            wikiMemory.wikiMemoryContentList.Clear();
        }
        else if (wikiMemory != null && resetWikiMemory == false)
        {
            // wikiMemory.wikiMemoryContentList.AddRange(tooltipContentList);
        }
    }

    private void Update()
    {
        AddEntriesToWikiMemory();
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
            if (entry.Keyword == keyword)
            {
                if (!tooltipContainer.gameObject.activeInHierarchy)
                {
                    tooltipContainer.transform.position = mousePos + new Vector3(0, 450, 0);
                    tooltipContainer.gameObject.SetActive(true);
                }
                tooltipTitleTMP.text = entry.Keyword;
                tooltipDescriptionTMP.text = entry.Description;
                return;
            }
        }

        Debug.Log($"Keyword: {keyword} not found");
    }

    public TooltipInfos GetTooltipInfoByTitle(string title)
    {
        foreach (var entry in wikiMemory.wikiMemoryContentList)
        {
            // Using StringComparison.OrdinalIgnoreCase for case-insensitive comparison
            if (string.Equals(entry.Keyword, title, StringComparison.OrdinalIgnoreCase))
            {
                return entry;
            }
        }

        // Return an empty TooltipInfos or handle the case where the title is not found
        return new TooltipInfos();
    }

    public void CloseTooltip()
    {
        if (tooltipContainer.gameObject.activeInHierarchy)
            tooltipContainer.gameObject.SetActive(false);
    }

    // Method to add entries to wikimemory based on line index
    public void AddEntriesToWikiMemory()
    {
        if (simpleDialogue != null)
        {
            int currentLineIndex = simpleDialogue.index; // Check the property name in your SimpleDialogue script

            foreach (var entry in tooltipContentList)
            {
                if (currentLineIndex >= entry.LineIndex)
                {
                    // Check if the entry is not already in wikimemory
                    if (!wikiMemory.wikiMemoryContentList.Exists(e => e.Keyword == entry.Keyword))
                    {
                        // Add the entry to wikimemory
                        wikiMemory.wikiMemoryContentList.Add(new TooltipInfos
                        {
                            Keyword = entry.Keyword,
                            Description = entry.Description,
                            LineIndex = entry.LineIndex
                        });

                        Debug.Log($"Added {entry.Keyword} to wikimemory at line {currentLineIndex}");
                    }
                }
            }
        }
        else
        {
            //Debug.LogError("SimpleDialogue reference is not set!");
        }
    }
}
