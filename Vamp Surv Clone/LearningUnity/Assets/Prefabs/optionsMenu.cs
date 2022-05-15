using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionsMenu : MonoBehaviour
{
    public GameObject options;

    private bool isActive;

    private void Start()
    {
        isActive = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive == true) {
                options.SetActive(false);
                resume();
                isActive = false;
            } else {
                options.SetActive(true);
                pause();
                isActive = true;
            }
            
        }
    }

    public void exitOptions() {
        options.SetActive(false);
        resume();
    }

    private void pause()
    {
        Time.timeScale = 0;
    }

    private void resume() {
        Time.timeScale = 1;
    }
}
