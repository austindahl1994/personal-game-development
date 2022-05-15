using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSlimeSkull : MonoBehaviour
{
    private GameObject enemy;
    public EnemyStats stats;
    Rigidbody2D rb;

    private float startTime = 0.05f;
    private float timebetween;
    public GameObject echo;

    private Vector2 initialPosition;
    private Vector2 direction;
    private float speed;

    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, 1f));
        damage = stats.damage * 50;
        timebetween = startTime;
        speed = stats.speed * 4;
        rb = this.GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("Player");
        initialPosition = gameObject.transform.position;
        direction = new Vector2(enemy.transform.position.x, enemy.transform.position.y) - initialPosition;
        direction.Normalize();
        moveEnemy(direction);
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        Echo();
        moveEnemy(direction);
    }

    public void moveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-damage);
            Destroy(this.gameObject);
        }
    }

    public void Echo() {
        if (timebetween <= 0)
        {
            GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
            timebetween = startTime;
            Destroy(instance, 0.3f);
        }
        else
        {
            timebetween -= Time.deltaTime;
        }
    }
}

