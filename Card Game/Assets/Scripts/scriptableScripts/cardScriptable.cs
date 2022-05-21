using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class cardScriptable : ScriptableObject
{
    public new string name;
    [TextArea(15, 20)]
    public string description;

    public Sprite artwork;

    public bool typeTarget; 
    public int manaCost;
    public int attack;
    public int aoeAttack;
    public int defense;
    public int bleed;
    public int poison;
    public int burn; //Does no damage but ignite deals the amount of damage, +initial
    public bool ignite; //for burn
    public bool special;
    public string specialName;
    public bool hasStartOfTurnEffect;
    public string startOfTurnEffectName;
    public bool hasEndOfTurnEffect;
    public string endOfTurnEffectName;
}
