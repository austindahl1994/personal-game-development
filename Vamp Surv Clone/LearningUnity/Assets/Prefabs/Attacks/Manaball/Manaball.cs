using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manaball : MonoBehaviour
{
    Rigidbody2D rb;
    private GameObject enemy;

    private Vector2 moveDirection;
    private Vector2 target;
    private int damage;

    public ProjectileStats stats;
    [SerializeField]
    public GameObject floatingPoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        heatSeeking();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
            damage = Random.Range(stats.lowDamage, stats.highDamage + 1);
            collision.gameObject.GetComponent<EnemyHealth>().UpdateEnemyHealth(-damage);
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
            points.transform.GetComponent<TextMesh>().text = damage.ToString();
        }
    }

    public void heatSeeking()
    {
        enemy = FindClosestEnemy(1, 1000);
        if (!enemy)
        {
            target.x = (Random.Range(-10, 10));
            target.y = (Random.Range(-6, 6));
        }
        else
        {
            target = enemy.transform.position;
        }
        moveDirection.x = (target.x - transform.position.x);
        moveDirection.y = (target.y - transform.position.y);

        moveDirection.Normalize();

        rb.velocity = new Vector2(moveDirection.x * stats.speed * Time.fixedDeltaTime, moveDirection.y * stats.speed * Time.fixedDeltaTime);
        Destroy(gameObject, 4f);
    }

    public GameObject FindClosestEnemy(float min, float max)
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        min = min * min;
        max = max * max;
        foreach (GameObject x in allEnemies)
        {
            Vector3 difference = x.transform.position - position;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance && currentDistance >= min && currentDistance <= max)
            {
                closest = x;
                distance = currentDistance;
            }
        }
        return closest;
    }
}
