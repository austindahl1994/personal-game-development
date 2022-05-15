using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileStats : ScriptableObject
{
    public float level;
    public int lowDamage;
    public int highDamage;
    public int speed;
    public int pierce;
    public int size;
    public float fireRate;
    //used for say icicle variant, to designate time and distance between them spawning
    public float timeBetweenVariants;
    public float distanceBetweenVariants;
}
