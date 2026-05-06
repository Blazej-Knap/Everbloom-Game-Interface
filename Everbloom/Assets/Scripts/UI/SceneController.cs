using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadCalendar()
    {
        SceneManager.LoadScene("Calendar");
    }

    public void LoadDaySummary()
    {
        SceneManager.LoadScene("DaySummary");
    }

    void Update()
    {
        // Global 'X' to exit Calendar if we are in that scene
        if (SceneManager.GetActiveScene().name == "Calendar" && Input.GetKeyDown(KeyCode.X))
        {
            LoadGameScene();
        }
    }
}
