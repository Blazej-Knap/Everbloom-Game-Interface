using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public KeyCode toggleKey = KeyCode.E;

    void Start()
    {
        if (inventoryCanvas != null)
        {
            inventoryCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (inventoryCanvas != null)
        {
            bool isActive = inventoryCanvas.activeSelf;
            inventoryCanvas.SetActive(!isActive);

            // Optional: Pause game or lock cursor
            // if (!isActive) Time.timeScale = 0f;
            // else Time.timeScale = 1f;
        }
    }
}
