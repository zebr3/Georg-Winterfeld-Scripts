using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecruitedNpcData", menuName = "ScriptableObjects/RecruitedNpcScriptableObject")]
public class RecruitedNpcScriptableObject : ScriptableObject
{
    public List<NPCData> team = new List<NPCData>();
    RecruitmentManager recruitmentManager;

    public List<NPCData> specialCards = new List<NPCData>();
    public int chapterIndex;
    public bool tutorial;

    
}
