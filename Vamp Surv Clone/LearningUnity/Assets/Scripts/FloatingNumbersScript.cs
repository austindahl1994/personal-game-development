using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNumbersScript : MonoBehaviour
{
    //is the instantiated numbers that pop up when damaging enemies
    public GameObject numbers;
    float temp = 0.7f;
    void Update()
    {
        numbers.transform.localScale = new Vector3(temp, temp, 0);
        temp -= 0.002f;
        if (temp <= 0) {
            Destroy(gameObject, 0f);
        }
    }
}
