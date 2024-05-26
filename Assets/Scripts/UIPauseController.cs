using System;
using UnityEngine;

public class UIPauseController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanelUI;

    public static event Action OnPauseGame;
    public static event Action OnResumeGame;

    private bool isGameOver = false;
    private bool gameIsPaused = false;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    void Update()
    {
        PressESCToPause();
    }

    private void OnEnable()
    {
        GameController.OnGameOver += ChangeGameOverState;
    }

    private void OnDisable()
    {
        GameController.OnGameOver -= ChangeGameOverState;
    }

    private void PressESCToPause()
    {
        if (isGameOver)
        {
            return;
        }

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

    private void ChangeGameOverState() => isGameOver = true;

    private void Pause()
    {
        pausePanelUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        audioManager.SetVolume(AudioManager.GAME_BACKGROUND_AUDIO, 0.25f);
    }

    private void Resume()
    {
        pausePanelUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        audioManager.SetVolume(AudioManager.GAME_BACKGROUND_AUDIO, 0.8f);
    }
}
