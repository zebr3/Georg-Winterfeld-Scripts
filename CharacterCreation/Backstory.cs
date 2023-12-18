using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Backstory : MonoBehaviour, IPointerClickHandler
{
   
    [TextArea(3, 3)]
    public string[] text;
    public int index;
    [SerializeField] TextMeshProUGUI textOfBackstory;

    public void OnPointerClick(PointerEventData eventData)
    {
        //select new field and overwrite the data in image selection
        GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>().creationContainer = gameObject.GetComponent<CreationContainer>();
        GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>().currentIndex = index;
        GameObject.Find("ImageSelection").GetComponent<ChangeImageSelection>().textOfImage.text = string.Empty;



    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


}
