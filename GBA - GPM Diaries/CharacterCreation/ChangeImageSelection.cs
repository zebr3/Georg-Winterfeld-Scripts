using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeImageSelection : MonoBehaviour
{
    public GameObject startContainer;
    public CreationContainer creationContainer;
    public TextMeshProUGUI textOfImage;
    public int currentIndex = 0;
    int previousIndex = 0;
    public int direction;

    void Start()
    {
        creationContainer = startContainer.GetComponent<CreationContainer>();
        textOfImage.text = creationContainer.text[0];
       
    }



    public void UpdateSelection()
    {
        //save previousindex for image deactivation
        previousIndex = currentIndex;

        //increase index by one and change index of the container
        currentIndex += direction;
        creationContainer.index = currentIndex;

        //if index is out of bounds, reset it
        if (currentIndex >= creationContainer.images.Length)
        {
            currentIndex = 0;
            creationContainer.index = currentIndex;
        }

        if (currentIndex < 0)
        {
            currentIndex = creationContainer.images.Length - 1;
            creationContainer.index = currentIndex;

        }

        //deactivate previous image
        creationContainer.images[previousIndex].SetActive(false);
        

       //activate new image
        creationContainer.images[currentIndex].SetActive(true);
        textOfImage.text = creationContainer.text[currentIndex].ToString();
    }

   void Update()
    {
       
    }
}
