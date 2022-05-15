using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attractor : MonoBehaviour
{
    CircleCollider2D cc;
    public playerStats stats;

    private void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        cc.radius = stats.itemGatherRadius;
    }
    private void Update() //remove this, add item that calls pullAll, this is for testing purposes
    {
        if (Input.GetKeyDown("r")) {
            StartCoroutine(pullAll());
        }
    }

    public IEnumerator pullAll() {
        cc.radius = 150;
        yield return new WaitForSeconds(0.1f);
        cc.radius = stats.itemGatherRadius;
    }
}
