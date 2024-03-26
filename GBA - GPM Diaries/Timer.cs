using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;
    [SerializeField] public bool runTime;
    [SerializeField] bool loadScene;
    [SerializeField] string sceneName;
    [SerializeField] LevelTransition levelTransition;


    // Update is called once per frame
    void Update()
    {
        if (runTime == true)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                if(loadScene==true)
                {
                    if (sceneName != null)
                    {
                        Debug.Log("changeScene");
                        levelTransition.FadeLoadScene(sceneName);
                    }
                }
                remainingTime = 0;
                if(timerText != null)
                {
                    timerText.color = Color.red;
                }
               
                //GameOver
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            if (timerText != null)
            {
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}
