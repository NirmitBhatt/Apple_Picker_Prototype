using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseController : MonoBehaviour
{
    public static event Action OnPauseGame;
    public static event Action OnResumeGame;
    [SerializeField] GameObject pausePanelUI;
    bool gameIsPaused = false;
    private AudioManager audioManager;
    // Update is called once per frame

    private void Awake()
    {
        audioManager = AudioManager.instance;
    }
    void Update()
    {
        PressESCToPause();
    }

    private void PressESCToPause()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
                OnResumeGame?.Invoke();
            }
            else
            {
                Pause();
                OnPauseGame?.Invoke();
            }
        }
    }

    private void Pause()
    {
        pausePanelUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        audioManager.SetVolume("GameBackground", 0.25f);
    }

    private void Resume()
    {
        pausePanelUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        audioManager.SetVolume("GameBackground", 0.8f);
    }
}
