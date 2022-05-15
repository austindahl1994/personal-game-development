using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullEcho : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, 1f));
    }
}
