﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;

    private bool isPaused;

    private void Start()
    {
        TogglePauseUI(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                TogglePauseUI(true);
            }
            else
            {
                BackToTheGame();
            }
        }
    }

    public void Continuer()
    {
        PlayButtonSound();
        BackToTheGame();
    }

    public void ChargerMenu()
    {
        PlayButtonSound();

        SceneManager.LoadScene(1);
    }

    private void BackToTheGame()
    {
        PlayButtonSound();

        TogglePauseUI(false);
    }

    private void PlayButtonSound()
    {
        AkSoundEngine.PostEvent("playButton", gameObject);
    }

    private void TogglePauseUI(bool isActive)
    {
        isPaused = isActive;
        pauseMenuCanvas.SetActive(isActive);

        Time.timeScale = isActive ? 0 : 1;
    }
}
