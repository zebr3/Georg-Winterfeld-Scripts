using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button backButton;
    public Toggle soundToggle;
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    public TextSpeed getTextSpeed;
    public bool isPaused = false;
    private Button lastPressedButton;
    [SerializeField] Animator animator;
    [SerializeField] GameObject PageArea;
    [SerializeField] GameObject TabArea;
    [SerializeField] GameObject CloseButton;

    [SerializeField] Button optionButton;

   

    public bool initializeToggle;

    private void Start()
    {
        backButton.onClick.AddListener(ReturnToMainMenu);
        soundToggle.onValueChanged.AddListener(ToggleSound);

        // Read initial values from the AudioMixer and set UI elements
        InitializeUIValues();
       
    }

    private void InitializeUIValues()
    {
        // Read and set volume from AudioMixer
        float initialVolume;
        audioMixer.GetFloat("Volume", out initialVolume);
        volumeSlider.value = Mathf.Pow(10, initialVolume / 20);

        // Read and set sound toggle state from AudioMixer
        float initialMusicVolume;
        audioMixer.GetFloat("Music", out initialMusicVolume);
        if(initialVolume > 0)
        {
            soundToggle.isOn = true;
        } 
        else if(initialVolume <= 0) 
        {
            soundToggle.isOn = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    

    public void setPause(bool pause)
    {
        if(pause == true)
        {
            isPaused = true;
        } else if (pause == false)
        {
            isPaused = false;
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
        // Disable other objects or functionalities here
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        // Enable other objects or functionalities here
    }

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("0_MainMenu"); // Replace "MainMenu" with the name of your main menu scene.
    }

    public void PlayOptionMenuAnimation()
    {
        animator.Play("TabMenuAnimation", 0, 0);
    }

    public void PlayOptionMenuCloseAnimation()
    {
        animator.Play("TabMenuCloseAnimation", 0, 0);
    }

    public void OnAnimationEnd()
    {
        PageArea.SetActive(true);
        TabArea.SetActive(true);
        CloseButton.SetActive(true);
        optionButton.interactable = false;
    }
    public void OnCloseAnimationEnd()
    {
        PageArea.SetActive(false);
        TabArea.SetActive(false);
        CloseButton.SetActive(false);
        optionButton.interactable = true;
    }


    public void EnableOptionButton()
    {
        optionButton.interactable = true;
    }


    private void ToggleSound(bool isOn)
    {
        if (isOn == true)
        {
            audioMixer.SetFloat("Music", 0f); // Set the volume parameter to 0 (full volume)
        }
        else
        {
            audioMixer.SetFloat("Music", -80f); // Set the volume parameter to -80 (mute)
        }
    }

    public void SetTextSpeed(string textSpeed)
    {
        if (lastPressedButton != null)
        {
            lastPressedButton.interactable = true; // Enable the last pressed button
        }

        if (textSpeed == "slow")
        {
            getTextSpeed.speed = 0.06f;
        }
        else if (textSpeed == "standard")
        {
            getTextSpeed.speed = 0.04f;
        }
        else if (textSpeed == "fast")
        {
            getTextSpeed.speed = 0.02f;
        }

        Button currentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        currentButton.interactable = false; // Disable the currently pressed button
        lastPressedButton = currentButton; // Update the last pressed button
    }
}
