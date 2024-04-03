using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseController : MonoBehaviour
{
    [SerializeField] GameObject pausePanelUI;
    bool gameIsPaused = false;
    // Update is called once per frame
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
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pausePanelUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void Resume()
    {
        pausePanelUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
