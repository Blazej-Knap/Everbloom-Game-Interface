using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToGame : MonoBehaviour
{
    public void Go()
    {
        SceneManager.LoadScene("GameScene");
    }
}
