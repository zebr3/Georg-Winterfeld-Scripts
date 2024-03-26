using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WikiMenu : MonoBehaviour
{
    [SerializeField] public WikiMemory wikiMemory;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming you have a reference to the TooltipHandlerHover script
       // wikiList = GetComponent<TooltipHandlerHover>();

        // Call the method to change names in children
        ChangeNamesInChildren();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeNamesInChildren();
    }
    void ChangeNamesInChildren()
    {
        int tooltipCount = wikiMemory.wikiMemoryContentList.Count;

        // Iterate through all child GameObjects
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the current child GameObject
            Transform child = transform.GetChild(i);

            // Check if the index is within the bounds of the tooltipContentList
            if (i < tooltipCount)
            {
                // Find the TMP_Text component in the child hierarchy
                TMP_Text text = child.GetComponentInChildren<TMP_Text>(true);

                // Check if the child has a TMP_Text component
                if (text != null)
                {
                    // Change the text based on the tooltipContentList
                    text.text = wikiMemory.wikiMemoryContentList[i].Keyword;
                }

                // Activate the child if it was deactivated
                child.gameObject.SetActive(true);
            }
            else
            {
                // Deactivate or destroy the unnecessary child objects
                child.gameObject.SetActive(false); // or Destroy(child.gameObject);
            }
        }
    }

}
