using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviour
{
    public TMP_InputField farmNameInput;
    public TMP_Text errorMessage;
    public GameObject errorGroup; // Obiekt zawierający ikonę i tekst błędu
    public Button startGameButton;

    private void Awake()
    {
        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(OnStartGameClick);
        }
    }

    private void Start()
    {
        Debug.Log("NewGameManager started. Checking EventSystem...");
        if (UnityEngine.EventSystems.EventSystem.current == null)
        {
            Debug.LogWarning("WARNING: No EventSystem found in scene! UI buttons will not work.");
        }

        if (errorGroup != null)
            errorGroup.SetActive(false);
        else if (errorMessage != null)
            errorMessage.text = "";
    }

    public void OnStartGameClick()
    {
        Debug.Log("OnStartGameClick triggered");
        if (farmNameInput == null)
        {
            Debug.LogError("farmNameInput is not assigned in the Inspector!");
            return;
        }

        if (string.IsNullOrWhiteSpace(farmNameInput.text))
        {
            Debug.Log("Farm name is empty");
            if (errorGroup != null)
            {
                errorGroup.SetActive(true);
                if (errorMessage != null)
                {
                    errorMessage.text = "Błąd! Brak nazwy farmy!";
                }
            }
            else if (errorMessage != null)
            {
                errorMessage.text = "Błąd! Brak nazwy farmy!";
                errorMessage.color = Color.red;
            }
        }
        else
        {
            // Transition to new scene
            Debug.Log("Starting game with farm: " + farmNameInput.text);
            SceneManager.LoadScene("GameScene");
        }
    }
}
