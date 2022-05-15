using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject enemy;

    private int randox;
    private int randoy;
    private int damage;

    public ProjectileStats stats;
    [SerializeField]
    public GameObject floatingPoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireAxis();
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

    public void fireAxis()
    {
        randox = Random.Range(-1, 2);
        randoy = Random.Range(-1, 2);
        if (randox == 0 && randoy == 0) {
            randox = 1;
        }
        rb.AddForce(new Vector2(randox*stats.speed * 50*Time.fixedDeltaTime, randoy*stats.speed*50* Time.fixedDeltaTime));
        rb.AddTorque(360, ForceMode2D.Impulse);
        Destroy(gameObject, 4.5f);
    }
}
