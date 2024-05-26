using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    private const string GAME_SCENE_NAME = "Game Scene";
    private const string MAIN_MENU_SCENE_NAME = "Main Menu";

    public void LoadGame()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#endif
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        Time.timeScale = 1.0f;
        GameController.OnGameOver?.Invoke();
    }
}
