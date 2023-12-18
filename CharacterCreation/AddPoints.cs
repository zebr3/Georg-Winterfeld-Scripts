using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AddPoints : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int skillIndex;
    [SerializeField] int direction;
    SkillDistribution skillDistribution;
   

    void Start()
    {
        skillDistribution = GameObject.Find("Skills").GetComponent<SkillDistribution>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        skillDistribution.availablePoints -= direction;


        skillDistribution.direction = direction;
        skillDistribution.selectedSkill = skillIndex;
        skillDistribution.UpdateText();

        
    }

    void Update()
    {
        if(skillDistribution.availablePoints == 6 && direction == -1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
       
    }
}
