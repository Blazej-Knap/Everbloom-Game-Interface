using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject toolbar;
    public GameObject dialogueCanvas;

    private bool isDialogueActive = false;
    private bool closedThisFrame = false;

    public bool IsDialogueActive => isDialogueActive;
    public bool ClosedThisFrame => closedThisFrame;

    void Start()
    {
        if (dialogueCanvas != null) dialogueCanvas.SetActive(false);
    }

    public void ShowDialogue()
    {
        if (toolbar != null) toolbar.SetActive(false);
        if (dialogueCanvas != null) dialogueCanvas.SetActive(true);
        isDialogueActive = true;
    }

    public void HideDialogue()
    {
        if (dialogueCanvas != null) dialogueCanvas.SetActive(false);
        if (toolbar != null) toolbar.SetActive(true);
        isDialogueActive = false;
    }

    void Update()
    {
        closedThisFrame = false;
        if (isDialogueActive)
        {
            // "po kliknięciu bylejakich klawiszy chowa dialog i pokazuje toolbar znowu"
            if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
            {
                HideDialogue();
                closedThisFrame = true;
            }
        }
    }
}
