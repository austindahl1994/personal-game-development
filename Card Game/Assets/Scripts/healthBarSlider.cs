using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class healthBarSlider : MonoBehaviour
{
    public Image hpBar;
    public TMP_Text hpText;

    public void setHealth(float health, float maxHealth)
    {
        hpBar.fillAmount = health / maxHealth;
        hpText.text = health.ToString() + "/" + maxHealth.ToString();
    }
}
