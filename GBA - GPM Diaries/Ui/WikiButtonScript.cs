using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WikiButtonScript : MonoBehaviour
{
    [SerializeField]  TMP_Text titleObject;
    [SerializeField]  TMP_Text descriptionText;

    private void Start()
    {
        // Assuming you have a reference to TooltipHandlerHover
        TooltipHandlerHover tooltipHandler = FindObjectOfType<TooltipHandlerHover>();

        // Set up the button click listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClick(tooltipHandler));
        }
    }

    private void OnButtonClick(TooltipHandlerHover tooltipHandler)
    {
        // Check if TooltipHandlerHover reference is available
        if (tooltipHandler != null)
        {
            // Get the title from the child TMP_Text component
            TMP_Text titleText = GetComponentInChildren<TMP_Text>();

            if (titleText != null)
            {
                string buttonTitle = titleText.text;

                // Get the tooltip info for the button title
                TooltipInfos tooltipInfo = tooltipHandler.GetTooltipInfoByTitle(buttonTitle);
                    
                // Assuming you have references to TMP_Text components
                TMP_Text titleDisplayText = titleObject;  /* reference to title TMP_Text */ 
                TMP_Text descriptionDisplayText = descriptionText; /* reference to description TMP_Text */
 
                // Update the TMP_Text components with the tooltip info
                titleDisplayText.text = tooltipInfo.Keyword;
                descriptionDisplayText.text = tooltipInfo.Description;
            }
            else
            {
                Debug.LogWarning("ButtonScript: No TMP_Text component found on the button.");
            }
        }
    }
}
