using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject newGamePanel;
    public GameObject settingsPanel;

    [Header("Buttons")]
    public Button newGameButton;
    public Button loadGameButton;
    public Button settingsButton;
    public Button exitButton;

    private void Awake()
    {
        // Remove existing listeners to avoid double-registration
        if (newGameButton != null) newGameButton.onClick.RemoveAllListeners();
        if (settingsButton != null) settingsButton.onClick.RemoveAllListeners();
        if (exitButton != null) exitButton.onClick.RemoveAllListeners();
        if (loadGameButton != null) loadGameButton.onClick.RemoveAllListeners();

        // Add fresh listeners
        if (newGameButton != null) newGameButton.onClick.AddListener(OnNewGameClick);
        if (settingsButton != null) settingsButton.onClick.AddListener(OnSettingsClick);
        if (exitButton != null) exitButton.onClick.AddListener(OnExitClick);
    }

    private void Start()
    {
        Debug.Log("MainMenuManager initialized.");
        ShowMainPanel();

        if (loadGameButton != null)
        {
            loadGameButton.interactable = false;
        }

        if (newGameButton != null) newGameButton.Select();
    }

    private void OnNewGameClick()
    {
        Debug.Log("New Game clicked.");
        if (newGamePanel != null)
        {
            mainPanel.SetActive(false);
            newGamePanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("NewGameMenu");
        }
    }

    private void OnSettingsClick()
    {
        Debug.Log("Settings clicked.");
        if (settingsPanel != null)
        {
            mainPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("SettingsMenu");
        }
    }

    private void OnExitClick()
    {
        Debug.Log("Exit clicked.");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ShowMainPanel()
    {
        if (mainPanel != null) mainPanel.SetActive(true);
        if (newGamePanel != null) newGamePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }
    }
