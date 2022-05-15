using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    //bat prefab script, follows player
    private Vector2 movement;
    private Vector3 playerPosition;
    private Vector3 currScale;

    private GameObject enemy;
    private GameObject player;
    public EnemyStats enemyStats;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        enemy = this.gameObject;
        currScale = enemy.gameObject.transform.localScale;
        player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {
        playerPosition = player.transform.position;
        Vector3 direction = playerPosition - transform.position;
        direction.Normalize();
        movement = direction;
        flipDirection();
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

    //flips direction (scale) depending if the player is to the right or left of enemy
    public void flipDirection()
    {
        if (playerPosition.x >= enemy.gameObject.transform.position.x)
        {
            if (enemy.gameObject.transform.localScale.x < 0)
            {
                enemy.gameObject.transform.localScale += new Vector3(currScale.x * 2, 0, 0);
            }
        }
        else
        {
            if (enemy.gameObject.transform.localScale.x > 0)
            {
                enemy.gameObject.transform.localScale += new Vector3(-currScale.x * 2, 0, 0);
            }
        }

    }
}
