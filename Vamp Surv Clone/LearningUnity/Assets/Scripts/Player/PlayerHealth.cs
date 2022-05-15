using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    public playerStats stats;
    public hpBar healthBar;
    [SerializeField]
    private float maxHealth = 100f;
    private SpriteRenderer sprite;
    private float timeSinceLastDmgTaken;

    private void Start()
    {
        stats.health = 100; //remove/change depending on character, using just for testing back to full HP
        health = maxHealth;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        timeSinceLastDmgTaken = 0;
    }

    public void UpdateHealth(float mod) {
        timeSinceLastDmgTaken += 0.5f * Time.deltaTime;
        StartCoroutine(resetColor(timeSinceLastDmgTaken));
        health += mod;
        if (health > maxHealth)
        {
            health = maxHealth;
            healthBar.setHealth(health);
            stats.health = ((int)health);
        }
        else if (health <= 0) {
            health = 0f;
            healthBar.setHealth(health);
            Debug.Log("player respawn");
            stats.health = ((int)health);
        }
        stats.health = ((int)health);
        healthBar.setHealth(health);
    }

    private IEnumerator resetColor(float takenTime)
    {
        this.sprite.color = Color.red;
        yield return new WaitForSeconds(takenTime);
        timeSinceLastDmgTaken = 0;
        this.sprite.color = Color.white;
        yield break;
    }
}
