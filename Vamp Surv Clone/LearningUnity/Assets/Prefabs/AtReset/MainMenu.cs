using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public void PlayGame() {
        StartCoroutine(playButton());
    }

    public void OptionsButton() {
        StartCoroutine(optionsButton());
    }

    public void QuitGame() {
        StartCoroutine(quitButton());
    }

    IEnumerator playButton() {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("CharacterSelect");
    }

    IEnumerator quitButton()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
    }

    IEnumerator optionsButton() {
        yield return new WaitForSeconds(0.3f);
        OptionsMenu.SetActive(true);
    }
}
