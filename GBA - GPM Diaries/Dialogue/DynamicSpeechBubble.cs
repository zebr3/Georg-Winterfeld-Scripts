using UnityEngine;
using TMPro;

public class DynamicSpeechBubble : MonoBehaviour
{
    public RectTransform bubbleBackground;
    public TextMeshProUGUI textComponent;
    public float padding = 20f;

    void Start()
    {
        AdjustSpeechBubbleSize();
    }

    void Update()
    {
        // Call the adjustment function whenever the text content changes
        AdjustSpeechBubbleSize();
    }

    void AdjustSpeechBubbleSize()
    {
        // Set the text content and reset the size of the speech bubble
        textComponent.ForceMeshUpdate();
        bubbleBackground.sizeDelta = new Vector2(textComponent.preferredWidth + padding, textComponent.preferredHeight + padding);
    }
}
