using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndSliceAnimation()
    {
        GetComponent<Animator>().SetTrigger("EndSlice");
        gameObject.SetActive(false);
    }
}
