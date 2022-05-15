using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choiceCanvas : MonoBehaviour
{
    public GameObject container;
    public Button choice1;
    public Button choice2;
    public Button choice3;
    // Start is called before the first frame update
    void Start()
    {
        container.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e")) {
            if (!container.activeSelf) {
                //pause();
                container.SetActive(true);
            }
        }
    }

    public void choiceOne() 
    {
        Debug.Log("Choice 1 chosen");
        container.SetActive(false);
        //resume();
    }

    public void choiceTwo()
    {
        Debug.Log("Choice 2 chosen");
        container.SetActive(false);
        //resume();
    }

    public void choiceThree()
    {
        Debug.Log("Choice 3 chosen");
        container.SetActive(false);
        //resume();
    }

    public void pause() {
        Time.timeScale = 0;
        Debug.Log("Game paused");
    }
    public void resume()
    {
        Time.timeScale = 1;
        Debug.Log("Game resumed");
    }
}
