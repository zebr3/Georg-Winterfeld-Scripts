using UnityEngine;
using Ink;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New NPC Data", menuName = "NPC Data")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public Sprite npcSprite;
    public string profession;
    [TextArea(5, 10)]
    public string anschreibText;
    public int npcStat;
    public bool IsRecruited;
    public Sprite portrait;
    public int npcDialogueIndex;

    //deckbuilder
    public int attack;
    public int weakAttack;
    public Sprite cardImage;
    public int erschöpfung;
    [Header("Sonderkarten")]
    public bool draw2;
    public bool plusOneAttack;
    public bool erschöpfungRegen;
    public bool minus3NextTurn;
}
