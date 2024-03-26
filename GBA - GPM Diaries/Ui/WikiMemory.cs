using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WikiMemory", menuName = "WikiMemory")]
public class WikiMemory : ScriptableObject
{
    [SerializeField] public List<TooltipInfos> wikiMemoryContentList;
}
