using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class experiencePoints : MonoBehaviour
{
    public Slider slider;
    public playerStats stats;
    public levelUp levelUp;
    // Start is called before the first frame update
    void Start()
    {
        stats.level = 0;
        stats.experiencePoints = 0;
        slider.minValue = 0;
        slider.maxValue = 1; // initial value for crystals needed to level up
    }

    // Update is called once per frame
    void Update()
    {
        updateBar();
    }

    public void updateBar() {
        slider.value = stats.experiencePoints;
        if (stats.experiencePoints >= slider.maxValue) {
            stats.experiencePoints = 0;
            stats.level++;
            levelUp.gainLevelUpChoice();
            slider.value = slider.minValue;
            slider.maxValue = (slider.maxValue) + slider.maxValue * 0.10f;
        }
    }
}
