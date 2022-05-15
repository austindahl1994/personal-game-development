using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{
    private float startTime = 0.1f;
    private float timebetween;
    public GameObject echo;

    private void Start()
    {
        timebetween = startTime;
    }
    private void Update()
    {
        if (timebetween <= 0)
        {
            GameObject instance = Instantiate(echo, transform.position, transform.rotation);
            timebetween = startTime;
            Destroy(instance, 0.3f);
        }
        else {
            timebetween -= Time.deltaTime;
        }
    }
}
