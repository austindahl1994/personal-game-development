using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaMine : MonoBehaviour
{
    public GameObject manaExplosion;
    private Animator anim;
    Rigidbody2D rb;
    private float angle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        angle = Random.Range(0, 360);
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180);
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180);
        rb.AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            anim.SetTrigger("wasTriggered");
        }
    }

    private void spawnExplosion() {
        Instantiate(manaExplosion, transform.position, Quaternion.identity);
    }
}
