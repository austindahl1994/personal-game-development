using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public GameObject floatingPoints;
    private int damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-damage);
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
            points.transform.GetComponent<TextMesh>().text = damage.ToString();
        }

        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().UpdateEnemyHealth(-damage);
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
            points.transform.GetComponent<TextMesh>().text = damage.ToString();
        }
    }
}
