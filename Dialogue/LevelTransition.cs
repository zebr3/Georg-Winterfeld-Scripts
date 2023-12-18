using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string sceneName;
    //public GameObject timer;
    //private Timer _timerScript;
    public float fadeDuration = 1.0f;
    private CanvasGroup fadeOverlay;
    [SerializeField] Animator transitionAnim;

    public void Start()
    {
      // _timerScript = timer.GetComponent<Timer>();
    }
   
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void FadedLoadScene(string sceneName)
    {
        StartCoroutine(FadeLoadScene(sceneName));
    }

    public IEnumerator FadeLoadScene(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        transitionAnim.SetTrigger("Start");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger entered");

        if(other.tag == "Player")
        {

            //_timerScript.runTime = false;
            //print("you completed with " + ((int)_timerScript.remainingTime) + " seconds left!");
            //print("switching scene to " + sceneName);
           // SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        print("trigger exit");

        if(other.tag == "Player")
        {
            //_timerScript.runTime = true;
            print("countdown resumed");
        }
    }
}
