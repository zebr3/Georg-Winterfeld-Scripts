using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Aufgabe", menuName = "AufgabeSO")]
public class AufgabenSO : ScriptableObject
{
    public int health;
    public int countdown;
    public Sprite sprite;
    public string profession;
    public string secondProfession;
}
