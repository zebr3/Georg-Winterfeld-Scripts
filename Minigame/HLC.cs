using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HLC : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float speed = 1f;
    bool once = true;
    
    public int correctParts;

    

   

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(1, 0) * speed;

        if(transform.position.x >= 7 && once)
        {
            if(correctParts == 4)
            {
                Debug.Log("gute");
                GameObject.FindWithTag("Manager").GetComponent<HLCMinigame>().hlcCounter += 1;
                GameObject.FindWithTag("Manager").GetComponent<HLCMinigame>().allowSpawn = true;
                once = false;

            }
            else
            {
                Debug.Log("schredder");
                GameObject.FindWithTag("Manager").GetComponent<HLCMinigame>().allowSpawn = true;
                once = false;
            }
        }
    }
}
