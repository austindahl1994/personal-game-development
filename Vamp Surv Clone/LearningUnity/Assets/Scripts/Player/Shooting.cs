using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //continuously creates different prefabs
    public GameObject manaBallPrefab;
    public ProjectileStats manaBallStats;

    public GameObject shurikenPrefab;
    public ProjectileStats shurikenStats;

    public GameObject earthDrillPrefab;
    public ProjectileStats earthDrillStats;

    public GameObject iciclePrefab;
    public ProjectileStats icicleStats;

    public GameObject manaMinePrefab;
    public ProjectileStats manaMineStats;

    private GameObject enemy;

    private void Start()
    {
        StartCoroutine(spawnFireballs());
        StartCoroutine(spawnShurikens());
        StartCoroutine(spawnEarthDrill());
        StartCoroutine(spawnIcicle());
        StartCoroutine(spawnManaMine());
    }

    //fire rate between 0.001 and 2 is time between shots
    private IEnumerator spawnFireballs()
    {
        yield return new WaitForSeconds(manaBallStats.fireRate);
        Instantiate(manaBallPrefab, new Vector2(gameObject.transform.position.x, 
            gameObject.transform.position.y), Quaternion.identity);
        StartCoroutine(spawnFireballs());
    }

    private IEnumerator spawnShurikens()
    {
        yield return new WaitForSeconds(shurikenStats.fireRate);
        Instantiate(shurikenPrefab, new Vector2(gameObject.transform.position.x, 
            gameObject.transform.position.y), Quaternion.identity);
        StartCoroutine(spawnShurikens());
    }

    private IEnumerator spawnEarthDrill() {
        yield return new WaitForSeconds(earthDrillStats.fireRate);
        if (GameObject.FindGameObjectWithTag("enemy"))
        {
            enemy = GameObject.FindGameObjectWithTag("enemy");
            Instantiate(earthDrillPrefab, new Vector2(enemy.transform.position.x,
            enemy.transform.position.y), Quaternion.identity);
        }
        StartCoroutine(spawnEarthDrill());
    }

    private IEnumerator spawnIcicle()
    {
        yield return new WaitForSeconds(icicleStats.fireRate);
        if (GameObject.FindGameObjectWithTag("enemy"))
        {
            Instantiate(iciclePrefab, new Vector2(gameObject.transform.position.x,
            gameObject.transform.position.y), Quaternion.identity);
        }
        StartCoroutine(spawnIcicle());
    }

    private IEnumerator spawnManaMine()
    {
        yield return new WaitForSeconds(manaMineStats.fireRate);
        Instantiate(manaMinePrefab, new Vector2(gameObject.transform.position.x,
            gameObject.transform.position.y), Quaternion.identity);
        StartCoroutine(spawnManaMine());
    }
}
