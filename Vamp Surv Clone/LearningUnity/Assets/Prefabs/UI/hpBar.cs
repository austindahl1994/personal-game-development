using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    public Slider slider;

    public void setHealth(float health) {
        slider.value = health;
    }
}
