using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hpValue : MonoBehaviour
{
    public TMP_Text text;
    public playerStats stats;
    private void Update()
    {
        text.text = stats.health.ToString();
    }
}
