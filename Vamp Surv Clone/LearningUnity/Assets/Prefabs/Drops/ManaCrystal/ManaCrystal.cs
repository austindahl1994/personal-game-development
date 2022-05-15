using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManaCrystal : MonoBehaviour
{
    Rigidbody2D rb;
    public InventoryStats inventory;
    public playerStats stats;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, 0.1f));
        rb = this.GetComponent<Rigidbody2D>();
        angle = Random.Range(0, 360);
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180);
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180);
        rb.AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        inventory.manaCrystal++;
        stats.experiencePoints++;
    }

}
