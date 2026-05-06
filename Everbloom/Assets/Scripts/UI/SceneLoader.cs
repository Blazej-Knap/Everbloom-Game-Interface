using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu() { SceneManager.LoadScene("MainMenu"); }
    public void LoadNewGameMenu() { SceneManager.LoadScene("NewGameMenu"); }
    public void LoadSettingsMenu() { SceneManager.LoadScene("SettingsMenu"); }
    public void LoadGameScene() { SceneManager.LoadScene("GameScene"); }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
