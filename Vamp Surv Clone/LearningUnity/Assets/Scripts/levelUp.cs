using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUp : MonoBehaviour
{
    public artifacts artifactScript;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject[] array = new GameObject[3];
    //public int[] array;
    public GameObject threeOptionsUI;
    public void gainLevelUpChoice() {
        array = getLevelUpOption();
        Debug.Log(array.Length);
        pause();
        threeOptionsUI.SetActive(true);
        Debug.Log("leveled up");
    }

    public void option1() {
        Debug.Log("option 1 chosen");
    }

    public void option2()
    {
        Debug.Log("option 2 chosen");
    }

    public void option3()
    {
        Debug.Log("option 3 chosen");
    }
    private void pause()
    {
        Time.timeScale = 0;
    }

    public void resume()
    {
        threeOptionsUI.SetActive(false);
        Time.timeScale = 1;
    }

    private GameObject[] getLevelUpOption() {
        for (int i = 0; i < 2; i++) { //change this to allow a new sized array for future to be able to give 4 options? 
            array[i] = artifactScript.getRandomListItem();
            artifactScript.removeObjectList(array[i]);
        }
        return array;
    }
}
