using UnityEngine;

/// <summary>
/// Globalny manager wejścia — trwa przez całą grę (wszystkie sceny).
/// Dodaj ten skrypt do GameObject w pierwszej scenie (np. MainMenu).
/// </summary>
public class GlobalInputManager : MonoBehaviour
{
    private static GlobalInputManager _instance;

    private void Awake()
    {
        // Singleton — tylko jedna instancja przez całą grę
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed — quitting game.");
            QuitGame();
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
