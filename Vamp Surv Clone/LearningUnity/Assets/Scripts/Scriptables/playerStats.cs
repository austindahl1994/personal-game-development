using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class playerStats : ScriptableObject
{
    public float speed;
    public float itemGatherRadius;
    public float health;
    public int level;
    public int experiencePoints;
    public int itemLuck;
    public int critLuck;
    public int crystalLuck;
}
