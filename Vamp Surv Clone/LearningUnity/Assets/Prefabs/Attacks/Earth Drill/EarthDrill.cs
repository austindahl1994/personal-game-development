using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDrill : MonoBehaviour
{
    public ProjectileStats stats;
    [SerializeField]
    public GameObject floatingPoints;

    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy") {
            damage = Random.Range(stats.lowDamage, stats.highDamage + 1);
            collision.gameObject.GetComponent<EnemyHealth>().UpdateEnemyHealth(-damage);
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
            points.transform.GetComponent<TextMesh>().text = damage.ToString();
        }
    }

    private void destroyDrill() {
        Destroy(gameObject);
    }
}
