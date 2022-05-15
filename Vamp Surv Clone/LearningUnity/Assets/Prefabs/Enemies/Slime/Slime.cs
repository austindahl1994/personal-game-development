using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private GameObject player;
    private Vector2 movement;
    private Vector3 playerPosition;

    public EnemyStats enemyStats;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        playerPosition = player.transform.position;
        Vector3 direction = playerPosition - transform.position;
        direction.Normalize();
        movement = direction;
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            moveEnemy(movement);
        }
    }

    public void moveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * enemyStats.speed * Time.deltaTime));
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-enemyStats.damage);
        }
    }
}
