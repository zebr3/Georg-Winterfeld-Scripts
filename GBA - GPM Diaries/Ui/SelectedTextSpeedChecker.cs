using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTextSpeedChecker : MonoBehaviour
{
    [SerializeField]
    private TextSpeed textSpeed; // Reference to the TextSpeed scriptable object

    // Start is called before the first frame update
    void Update()
    {
        CheckSelectedButton();
    }

    // Check the selected button based on the TextSpeed float value
    void CheckSelectedButton()
    {
        float targetSpeed = textSpeed.speed;

        // Iterate through child buttons
        foreach (Transform child in transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Check the button name to identify the speed
                string buttonName = button.gameObject.name;
                switch (buttonName)
                {
                    case "Schnell":
                        button.interactable = (targetSpeed != 0.02f); // Enable if not the target speed
                        break;
                    case "Standard":
                        button.interactable = (targetSpeed != 0.04f); // Enable if not the target speed
                        break;
                    case "Langsam":
                        button.interactable = (targetSpeed != 0.06f); // Enable if not the target speed
                        break;
                        // Add more cases as needed
                }
            }
        }
    }
}
