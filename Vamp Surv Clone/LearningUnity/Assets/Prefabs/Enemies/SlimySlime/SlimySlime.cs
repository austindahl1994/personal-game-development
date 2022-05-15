using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimySlime : MonoBehaviour
{
    public GameObject player;
    private Vector2 movement;
    private Vector3 playerPosition;
    private Animator anim;

    public EnemyStats enemyStats;
    private Rigidbody2D rb;

    private bool hasStarted = false;

    void Start()
    {
        anim = GetComponent<Animator>();
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
        else if (rb.bodyType == RigidbodyType2D.Static & hasStarted == false) {
            StartCoroutine(stickEm());
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

    public IEnumerator stickEm() {
        hasStarted = true;
        this.gameObject.transform.Translate(new Vector3(0, 0, 0.1f));
        yield return new WaitForSeconds(10f);
        anim.SetTrigger("actualDeath");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerMovement>().slowPlayer();
        }
    }
}
