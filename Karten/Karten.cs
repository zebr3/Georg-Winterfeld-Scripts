using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Karte")]
public class Karten : ScriptableObject 
{
    public new string name;
    public string type;
    public int attack;
    public int weakAttack = 1;
    public int motivation;

    public Sprite image;
}
