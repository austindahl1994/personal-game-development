using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaExplosion : MonoBehaviour
{
    public ProjectileStats stats;
    [SerializeField]
    public GameObject floatingPoints;

    private void Start()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, -1f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().UpdateEnemyHealth(-stats.highDamage);
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
            points.transform.GetComponent<TextMesh>().text = stats.highDamage.ToString();
        }
    }
}
