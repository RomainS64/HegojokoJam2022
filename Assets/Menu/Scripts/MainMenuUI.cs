using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject CreditsPanel;

    private void Start()
    {
        MainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        PlayButtonSound();
        //SceneManager.LoadScene("IntroDialogue");
    }


    public void Credits()
    {
        PlayButtonSound();
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        PlayButtonSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BackToMenu()
    {
        PlayButtonSound();
        CreditsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    private void PlayButtonSound()
    {
        //AudioManager.instance.PlayOneShot("UI_button");
    }
}
