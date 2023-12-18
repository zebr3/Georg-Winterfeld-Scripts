using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HLCMinigame : MonoBehaviour
{
    //vector2 for specific start location 
    Vector2 startPointPart = new Vector2(-9.3f, 3.6f);
    Vector2 startPointHLC = new Vector2(-6.8f, 0f);

    //gameobjects 
    [SerializeField] GameObject[] hlcPart;
    [SerializeField] GameObject hlcPrefab;

    //timer variables
    float startTime = 90;
    float timer;
    float hlcSpawnInterval = 2;
    float hlcPartTimer;
    float hlcTimer;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI hlcCounterText;

    public int hlcCounter;
    public bool allowSpawn = false;

    void Start()
    {
        timer = startTime;
        hlcPartTimer = hlcSpawnInterval;

        Invoke("SpawnHLC", 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        //text assignment
        hlcCounterText.text = hlcCounter.ToString() + "/8";
        timerText.text = timer.ToString("F0");

        //both timer
        timer -= Time.deltaTime;
        hlcPartTimer -= Time.deltaTime;

        //if timer is below 0, spawn next part and reset
        if(hlcPartTimer <= 0)
        {
            SpawnHLCPart();
            hlcPartTimer = hlcSpawnInterval;
        }

        //bool is true when hlc is at the end, spawn new hlc with 2 sec delay
        if(allowSpawn)
        {
            Invoke("SpawnHLC", 2);
            allowSpawn = false;
        }

        //end game and transition to next scene
        if(timer <= 0)
        {
            SceneManager.LoadScene("1.6 Message");
        }

    }

    //spawn empty hlc
    void SpawnHLC()
    {
        Instantiate(hlcPrefab, startPointHLC, Quaternion.identity);
    }

    //spawn random hlc part 
    void SpawnHLCPart()
    {
        int randomInt = Random.Range(0, 4);
        GameObject prefab = hlcPart[randomInt];
        Instantiate(prefab, startPointPart, Quaternion.identity);
    }
}
