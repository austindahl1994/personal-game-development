using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class enemyStats : ScriptableObject
{
    public new string name;
    public int startingBlock;
    public int damage;
    public int poisonDamage;
    public int health;
    public bool hasDeathEffect = false;
    public bool attackAction;
    public bool defendAction;
    public bool hybrid;
    public bool countdownAction;
    public bool poisonAction;
    public bool curseAction;
    public bool summonAction;
    public bool buffSelfAction;
    public bool buffOthersAction;
    public bool debuffAction;
    public bool healSelf;
    public bool healOthers;
    public Card canGiveCard;
}