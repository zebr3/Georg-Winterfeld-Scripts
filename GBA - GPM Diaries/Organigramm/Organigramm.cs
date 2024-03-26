using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Organigramm : MonoBehaviour
{
    public GameObject organiButton;
    public GameObject panel;
    public GameObject szene1;

    public int slotsAssigned;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slotsAssigned == 5)
        {
            gameObject.SetActive(false);
            organiButton.SetActive(false);
            panel.SetActive(false);
            szene1.SetActive(false);
            SceneManager.LoadScene("1_4Streit");
        }
    }
}
