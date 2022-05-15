using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class cardScriptable : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public bool typeTarget; 
    public int manaCost;
    public int attack;
    public int defense;
    public int bleed;
    public int poison;
}
