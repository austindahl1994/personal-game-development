using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icicle : MonoBehaviour
{
    private GameObject enemy;
    public ProjectileStats stats;
    public GameObject icicleVariant;
    [SerializeField]
    public GameObject floatingPoints;

    private Vector2 initialPosition;
    public Vector2 direction;

    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("enemy");
        initialPosition = gameObject.transform.position;
        direction = new Vector2 (enemy.transform.position.x, enemy.transform.position.y) - initialPosition;
        direction.Normalize();
        StartCoroutine(spawnVariant(initialPosition, direction));
        Destroy(gameObject, 5f);
    }


    private IEnumerator spawnVariant(Vector2 init ,Vector2 direction) {

        GameObject icicleExtra = Instantiate(icicleVariant, init, Quaternion.identity);
        Destroy(icicleExtra, 0.8f);
        init.x += (direction.x * stats.distanceBetweenVariants);
        init.y += (direction.y * stats.distanceBetweenVariants);
        yield return new WaitForSeconds(stats.timeBetweenVariants);
        StartCoroutine(spawnVariant(init ,direction));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            damage = Random.Range(stats.lowDamage, stats.highDamage + 1);
            collision.gameObject.GetComponent<EnemyHealth>().UpdateEnemyHealth(-damage);
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
            points.transform.GetComponent<TextMesh>().text = damage.ToString();
        }
    }

    private void destroyIcicle() {
        Destroy(gameObject);
    }

}
