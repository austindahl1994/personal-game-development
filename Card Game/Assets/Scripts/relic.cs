using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class relic : MonoBehaviour
{
    //have another class that manages relics
    public relicStats stats;
    public new TMP_Text name;
    public TMP_Text description;
    public Image relicArt;
    public int tier; //lower the better
    public bool isRelicActive;
    public Color bgColor;
    private void Start()
    {
        isRelicActive = false;
        name.text = stats.name;
        description.text = stats.desc;
        relicArt = stats.art;
        tier = stats.tier;

        if (tier == 0) {
            bgColor = Color.red;
        } else if (tier == 1) {
            bgColor = Color.blue;
        } else {
            bgColor = Color.white;
        }
    }
}
