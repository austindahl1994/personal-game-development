using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //updates enemy health, killing enemy if health reaches zero
    private float enemyHealth = 0f;
    public EnemyStats enemyStats;
    private Animator anim;
    private float enemyMaxHealth;

    Color initialColor;
    Rigidbody2D rb;
    Collider2D cc;
    private SpriteRenderer sprite;
    public GameObject floatingPoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<Collider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        initialColor = sprite.color;
        enemyMaxHealth = enemyStats.health;
        enemyHealth = enemyMaxHealth;
    }

    public void UpdateEnemyHealth(float mod)
    {
        StartCoroutine(resetColor());
        enemyHealth += mod;
        if (enemyHealth > enemyMaxHealth)
        {
            enemyHealth = enemyMaxHealth;
        }
        else if (enemyHealth <= 0)
        {
            enemyHealth = 0f;
            Die();
        }
    }

    void Die() {
        gameObject.tag = "Untagged";
        rb.bodyType = RigidbodyType2D.Static;
        cc.isTrigger = true;
        anim = GetComponent<Animator>();
        anim.SetTrigger("death");
    }

    private IEnumerator resetColor() {
        this.sprite.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        this.sprite.color = Color.white;
        yield break;
    }
}
