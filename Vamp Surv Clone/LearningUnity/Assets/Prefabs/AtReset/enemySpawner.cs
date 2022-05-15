using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public IEnumerator spawn() {
        Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
        yield break;
    }
}
