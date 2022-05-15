using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class expValue : MonoBehaviour
{
    public TMP_Text text;
    public playerStats stats;
    private void Update()
    {
        text.text = stats.level.ToString();
    }
}
