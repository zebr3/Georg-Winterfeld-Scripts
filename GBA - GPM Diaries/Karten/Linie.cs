using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Linie : MonoBehaviour
{
    [SerializeField] string[] freizeit;
    [SerializeField] string[] attack;
    [SerializeField] string[] weakAttack;
    [SerializeField] string[] erschoepfung;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Image profilImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnnouncementFreizeit(Sprite profil)
    {
        int announcementChange = Random.Range(0, 5);

        if(announcementChange == 0)
        {
            gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            int textInt = Random.Range(0, freizeit.Length);
            profilImage.sprite = profil;
            textMesh.text = freizeit[textInt];
        }
        
        

    }

    public void StartAnnouncementAttack(Sprite profil)
    {
        int announcementChange = Random.Range(0, 5);
        

        if (announcementChange == 0)
        {
            gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            int textInt = Random.Range(0, attack.Length);
            Debug.Log(textInt);
            profilImage.sprite = profil;
            textMesh.text = attack[textInt];
        }
    }

    public void StartAnnouncementWeakAttack(Sprite profil)
    {
        int announcementChange = Random.Range(0, 5);

        if (announcementChange == 0)
        {
            gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            int textInt = Random.Range(0, weakAttack.Length);
            profilImage.sprite = profil;
            textMesh.text = weakAttack[textInt];
        }
    }

    public void StartAnnouncementErschoepfung(Sprite profil)
    {
        int announcementChange = Random.Range(0, 5);

        if (announcementChange == 0)
        {
            gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            int textInt = Random.Range(0, erschoepfung.Length);
            profilImage.sprite = profil;
            textMesh.text = erschoepfung[textInt];
        }
    }
}
