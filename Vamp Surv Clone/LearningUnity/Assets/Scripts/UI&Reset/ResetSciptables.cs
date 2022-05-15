using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSciptables : MonoBehaviour
{
    //reset player stats


    //reset enemy stats


    //reset projectiles
    public ProjectileStats manaball;
    public ProjectileStats shuriken;
    public ProjectileStats icicle;
    public ProjectileStats earthDrill;

    private void Awake()
    {
        resetAll();
    }
    private void resetAll() {
        resetManaball();
        resetShuriken();
        resetIcicle();
        resetEarthDrill();
    }
    private void resetManaball() {
        manaball.level = 1;
        manaball.lowDamage = 2;
        manaball.highDamage = 7;
        manaball.speed = 4;
        manaball.pierce = 0;
        manaball.size = 1;
        manaball.fireRate = 5;
    }
    private void resetShuriken()
    {
        shuriken.level = 1;
        shuriken.lowDamage = 1;
        shuriken.highDamage = 2;
        shuriken.speed = 200;
        shuriken.pierce = 0;
        shuriken.size = 1;
        shuriken.fireRate = 10;
    }

    private void resetIcicle()
    {
        icicle.level = 1;
        icicle.lowDamage = 5;
        icicle.highDamage = 5;
        icicle.speed = 0;
        icicle.pierce = 0;
        icicle.size = 1;
        icicle.fireRate = 3;
        icicle.timeBetweenVariants = 0.2f;
        icicle.distanceBetweenVariants = 1f;
    }

    private void resetEarthDrill()
    {
        earthDrill.level = 1;
        earthDrill.lowDamage = 25;
        earthDrill.highDamage = 35;
        earthDrill.speed = 0;
        earthDrill.pierce = 0;
        earthDrill.size = 1;
        earthDrill.fireRate = 5;
        earthDrill.timeBetweenVariants = 0f;
        earthDrill.distanceBetweenVariants = 0f;
    }
}
