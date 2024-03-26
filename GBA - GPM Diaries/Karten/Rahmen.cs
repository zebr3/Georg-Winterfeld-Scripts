using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rahmen : MonoBehaviour
{
    public float fadeDuration = 2.0f; // Duration over which the object will fade out (in seconds)
    private float currentAlpha = 1.0f; // Starting alpha value
    private Image imageComponent;

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    // Coroutine to handle fading out
    IEnumerator FadeOut()
    {
        // Calculate the speed of fade based on the duration
        float fadeSpeed = 1.0f / fadeDuration;

        // Fade out loop
        while (currentAlpha > 0)
        {
            currentAlpha -= fadeSpeed * Time.deltaTime;
            SetAlpha(currentAlpha);
            yield return null; // Wait for the next frame
        }

        // Ensure the object is fully transparent
        currentAlpha = 0;
        SetAlpha(currentAlpha);
    }

    // Function to set the alpha of the image
    void SetAlpha(float alpha)
    {
        Color currentColor = imageComponent.color;
        currentColor.a = Mathf.Clamp01(alpha); // Ensure alpha is between 0 and 1


    }
}