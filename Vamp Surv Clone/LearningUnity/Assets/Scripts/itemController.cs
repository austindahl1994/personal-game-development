using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{
    Rigidbody2D rb;

    private Vector2 target;
    private Vector2 moveDirection;
    public playerStats stats;

    private float speed;


    private void Start()
    {
        speed = stats.speed + 3;
        this.gameObject.transform.Translate(new Vector3(0, 0, 0.1f));
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pickerUpper")
        {
            StartCoroutine(goToPlayer());
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator goToPlayer() {
        yield return new WaitForSeconds(0);
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        moveDirection.x = (target.x - transform.position.x);
        moveDirection.y = (target.y - transform.position.y);
        if (moveDirection.magnitude <= 1) {
            moveDirection.Normalize();
        }
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        StartCoroutine(goToPlayer());
    }
}
