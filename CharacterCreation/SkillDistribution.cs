using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillDistribution : MonoBehaviour
{
    [SerializeField] int[] skills;

    public int selectedSkill;

    [SerializeField] TextMeshProUGUI[] skillValueText;
    public int direction;

    public int availablePoints = 6;
   

    // Start is called before the first frame update
    void Start()
    {
        skills = new int[5];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateText()
    {
        
           

            skills[selectedSkill] += direction;
            skillValueText[selectedSkill].text = skills[selectedSkill].ToString();
           
        

       
    }
}
