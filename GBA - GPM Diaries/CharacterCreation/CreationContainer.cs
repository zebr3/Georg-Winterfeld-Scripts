using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CreationContainer : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] images;
    public GameObject highlight;
    public GameObject pfeile;
    [TextArea(3, 3)]
    public string[] text;
    public int index;

    public void OnPointerClick(PointerEventData eventData)
    {
        //select new field and overwrite the data in image selection
        GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>().creationContainer.highlight.SetActive(false);
        GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>().creationContainer.pfeile.SetActive(false);
        highlight.SetActive(true);
        pfeile.SetActive(true);
        GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>().creationContainer = gameObject.GetComponent<CreationContainer>();
        GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>().currentIndex = index;
        GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>().textOfImage.text = text[index];



    }

    void Start()
    {
        //initialize array size and store each child object into it
        images = new GameObject[transform.childCount - 4];
        for (int i = 0; i < transform.childCount - 4; i++)
        {
            images[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   
}
