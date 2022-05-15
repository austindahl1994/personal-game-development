using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //starts coroutine that countinuously calls itself, spawning enemies in position around player
    private GameObject player;
    Coroutine co = null;
    private Vector2 spawnPosition;
    private Vector2 playerPosition;
    private Vector2[] vecArray;
    private float speed = 2.4f;
    private int index;
    private bool spawnBool;

    //spawns prefab
    [SerializeField] public GameObject[] enemy;
    [SerializeField] public GameObject[] testEnemy;

    //spawn time for prefabs
    [SerializeField] public float slimeInterval = 0.01f;
    [SerializeField] public float testEnemyInterval = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        spawnBool = false;
        co = StartCoroutine(testSpawn(enemy[index]));
        //StartCoroutine(spawnEnemy(slimeInterval, enemy[index]));
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Update()
    {
        if (Input.GetKeyDown("space")){
            //Debug.Log("space was pressed");
            StopCoroutine(co);
            spawnBool = true;
        }

        if (Input.GetKeyDown("e")) {
            StopCoroutine(co);
            spawnBool = true;
            if (index == (enemy.Length - 1)) {
                index = 0;
                spawnBool = false;
                co = StartCoroutine(spawnAllTypes(enemy));
            } else {
                spawnBool = false;
                Debug.Log("e was pressed");
                co = StartCoroutine(testSpawn(enemy[index]));
                index++;
            }
        }
    }
    private void FixedUpdate()
    {
        playerPosition = player.transform.position;
        spawnPosition = arrayHelper(playerPosition);
    }

    private IEnumerator spawnEnemy(float interval, GameObject spawnedEnemy)
    {
        yield return new WaitForSeconds(interval - speed);
        
        GameObject newEnemy = Instantiate(spawnedEnemy, new Vector3(spawnPosition.x, spawnPosition.y, 0.0f), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, spawnedEnemy));
    }

    private IEnumerator testSpawn(GameObject spawnedEnemy) {
        if (spawnBool)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("test coroutine");

        GameObject newEnemy = Instantiate(spawnedEnemy, new Vector3(spawnPosition.x, spawnPosition.y, 0.0f), Quaternion.identity);
        StartCoroutine(testSpawn(spawnedEnemy));
    }

    private IEnumerator spawnAllTypes(GameObject[] spawnedEnemy) {
        if (spawnBool) {
            yield break;
        }
        
        yield return new WaitForSeconds(0.5f);
        
        GameObject newEnemy = Instantiate(spawnedEnemy[Random.Range(0, spawnedEnemy.Length)], new Vector3(spawnPosition.x, spawnPosition.y, 0.0f), Quaternion.identity);
        StartCoroutine(spawnAllTypes(spawnedEnemy));
    }

    public Vector2 arrayHelper(Vector2 pos) {
        vecArray = new Vector2[68];
        int index = 0;
        for (int i = -10; i <= 10; i++) {
            vecArray[index].x = i;
            vecArray[index].y = 6;
            index++;
        }
        for (int i = -10; i <= 10; i++)
        {
            vecArray[index].x = i;
            vecArray[index].y = -6;
            index++;
        }
        for (int i = -6; i <= 6; i++)
        {
            vecArray[index].x = 10;
            vecArray[index].y = i;
            index++;
        }
        for (int i = -6; i <= 6; i++)
        {
            vecArray[index].x = -10;
            vecArray[index].y = i;
            index++;
        }

        return (pos + vecArray[Random.Range(0, vecArray.Length)]);
    }
}