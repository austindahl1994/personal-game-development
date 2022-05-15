using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expBar : MonoBehaviour
{
    public Slider slider;

    public void setExp(int exp)
    {
        slider.value = exp;
    }
}


