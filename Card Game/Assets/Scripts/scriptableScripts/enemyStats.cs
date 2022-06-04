using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class enemyStats : ScriptableObject
{
    public new string name;
    public int actionsPerTurn;
    public int startingBlock;
    public int damage;
    public int defense;
    public int poisonDamage;
    public int health;
    public int burnDamage;
    public int regenSelfAmount;
    public int healSelfAmount;
    public int healOthersAmount;
    public int buffAmount;
    public int sunderAmount;
    public int weakenAmount;
    public bool corrosion;
    public bool buffSelfAction;
    public bool buffOthersAction;
    public bool hasDeathEffect = false;
    public bool retainsBlock = false;
    public bool countdownAction;
    public bool curseAction;
    public bool weakenAction;
    public bool sunderAction;
    public GameObject summon;
    public GameObject canGiveCard;
}