using UnityEngine;

public class DialogueToggle : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public KeyCode toggleKey = KeyCode.F; // F for Talk/Interaction

    void Start()
    {
        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (dialogueCanvas != null)
            {
                dialogueCanvas.SetActive(!dialogueCanvas.activeSelf);
            }
        }
    }
}
