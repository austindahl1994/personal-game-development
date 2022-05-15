using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSlime : MonoBehaviour
{
    private GameObject player;
    public GameObject skull;
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
        if (this.gameObject.tag == "Untagged") {
            Instantiate(skull, transform.position, Quaternion.identity);
            DropItem refscript = GetComponent<DropItem>();
            refscript.dropItem();
            Destroy(this.gameObject);
        }
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
