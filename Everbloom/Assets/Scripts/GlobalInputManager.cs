using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Globalny manager wejścia — trwa przez całą grę (wszystkie sceny).
/// Dodaj ten skrypt do GameObject w pierwszej scenie (np. MainMenu).
/// </summary>
public class GlobalInputManager : MonoBehaviour
{
    private static GlobalInputManager _instance;
    private GameObject _pauseCanvasInstance;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        ApplyAudioSettings();
        ApplyPixelPerfectFix();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        _pauseCanvasInstance = null; // Clear scene reference
        if (scene.name == "GameScene")
        {
            FindPauseCanvasInScene();
            if (_pauseCanvasInstance != null)
            {
                _pauseCanvasInstance.SetActive(false);
            }
        }
        ApplyAudioSettings();
        ApplyPixelPerfectFix();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                TogglePause();
            }
        }
    }

    private void TogglePause()
    {
        // Check if Dialogue with Luna is active
        DialogueController dialogueController = Object.FindAnyObjectByType<DialogueController>();
        if (dialogueController != null)
        {
            if (dialogueController.IsDialogueActive || dialogueController.ClosedThisFrame)
            {
                // Escape closed or is closing the dialogue, do not open pause menu
                return;
            }
        }

        // Check if Player Inventory is open or was closed this frame
        Kinnly.PlayerInventory playerInventory = Object.FindAnyObjectByType<Kinnly.PlayerInventory>();
        if (playerInventory != null)
        {
            if (playerInventory.IsInventoryOpen || playerInventory.ClosedThisFrame)
            {
                // Escape closed or is closing the inventory, do not open pause menu
                return;
            }
        }

        if (_pauseCanvasInstance == null)
        {
            FindPauseCanvasInScene();
        }

        if (_pauseCanvasInstance != null)
        {
            bool isPaused = !_pauseCanvasInstance.activeSelf;
            _pauseCanvasInstance.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
            Debug.Log($"[GlobalInputManager] Toggled pause to: {isPaused}");
        }
        else
        {
            Debug.LogWarning("[GlobalInputManager] TogglePause: PauseCanvas not found in the scene.");
        }
    }

    private void FindPauseCanvasInScene()
    {
        _pauseCanvasInstance = null;
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var go in allObjects)
        {
            if ((go.name == "PauseCanvas" || go.name == "pausecanvas") && go.scene.isLoaded)
            {
                _pauseCanvasInstance = go;
                
                // Automatically wire up any Resume and Exit buttons we find in the canvas
                UnityEngine.UI.Button[] buttons = go.GetComponentsInChildren<UnityEngine.UI.Button>(true);
                foreach (var btn in buttons)
                {
                    string btnName = btn.gameObject.name.ToLower();
                    if (btnName.Contains("resume") || btnName.Contains("wznów") || btnName.Contains("wznow"))
                    {
                        btn.onClick.RemoveListener(ResumeGame);
                        btn.onClick.AddListener(ResumeGame);
                        Debug.Log($"[GlobalInputManager] Dynamically bound ResumeGame to button: {btn.gameObject.name}");
                    }
                    else if (btnName.Contains("exit") || btnName.Contains("wyjdź") || btnName.Contains("wyjdz"))
                    {
                        btn.onClick.RemoveListener(ExitToMainMenu);
                        btn.onClick.AddListener(ExitToMainMenu);
                        Debug.Log($"[GlobalInputManager] Dynamically bound ExitToMainMenu to button: {btn.gameObject.name}");
                    }
                }
                break;
            }
        }
    }

    public void ResumeGame()
    {
        if (_pauseCanvasInstance != null)
        {
            _pauseCanvasInstance.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        _pauseCanvasInstance = null;
        SceneManager.LoadScene("MainMenu");
    }

    public static void ApplyAudioSettings()
    {
        float volume = PlayerPrefs.GetFloat("Settings_Glosnosc", 0.75f);
        bool musicOn = PlayerPrefs.GetInt("Settings_Muzyka", 1) == 1;
        bool soundOn = PlayerPrefs.GetInt("Settings_Dzwieki", 1) == 1;

        AudioListener.volume = volume;

        AudioSource[] sources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (var src in sources)
        {
            if (src == null) continue;
            bool isMusic = src.loop || 
                           (src.clip != null && (src.clip.name.ToLower().Contains("music") || src.clip.name.ToLower().Contains("bgm") || src.clip.name.ToLower().Contains("loop"))) ||
                           src.gameObject.name.ToLower().Contains("music") || 
                           src.gameObject.name.ToLower().Contains("bgm");

            if (isMusic)
            {
                src.mute = !musicOn;
            }
            else
            {
                src.mute = !soundOn;
            }
        }
    }

    public static void ApplyPixelPerfectFix()
    {
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            float targetOrthoSize = mainCam.orthographicSize;
            if (targetOrthoSize <= 0) targetOrthoSize = 5f;

            int ppu = 16;
            int refHeight = Mathf.RoundToInt(targetOrthoSize * 2f * ppu);
            int refWidth = Mathf.RoundToInt(refHeight * (16f / 9f));

            var ppc = mainCam.GetComponent<UnityEngine.Rendering.Universal.PixelPerfectCamera>();
            if (ppc == null)
            {
                ppc = mainCam.gameObject.AddComponent<UnityEngine.Rendering.Universal.PixelPerfectCamera>();
            }
            if (ppc != null)
            {
                ppc.assetsPPU = ppu;
                ppc.refResolutionX = refWidth;
                ppc.refResolutionY = refHeight;
                ppc.gridSnapping = UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.UpscaleRenderTexture;
                ppc.cropFrame = UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.StretchFill;
                Debug.Log($"[PixelPerfectFix] Configured camera: {refWidth}x{refHeight}");
            }
        }

        // Apply point filtering to all loaded textures to prevent bilinear bleeding seams
        SpriteRenderer[] spriteRenderers = Object.FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None);
        foreach (var sr in spriteRenderers)
        {
            if (sr != null && sr.sprite != null && sr.sprite.texture != null)
            {
                sr.sprite.texture.filterMode = FilterMode.Point;
            }
        }
    }
}
