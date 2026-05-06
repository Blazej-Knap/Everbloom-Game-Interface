using UnityEngine;
using UnityEngine.EventSystems;
using Kinnly;

public class InteractionBridge : MonoBehaviour, IPointerClickHandler, IInteractable
{
    public enum InteractionType { Calendar, DaySummary, LunaDialogue }
    public InteractionType type;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Interaction clicked: {gameObject.name} (Type: {type})");
        ExecuteInteraction();
    }

    public void Interact(PlayerInventory playerInventory)
    {
        Debug.Log($"Interaction triggered via 'E': {gameObject.name} (Type: {type})");
        ExecuteInteraction();
    }

    // Keep OnMouseDown for legacy support/ease of use
    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // Prevent double fire if clicked through UI
        {
            Debug.Log($"Interaction clicked (OnMouseDown): {gameObject.name} (Type: {type})");
            ExecuteInteraction();
        }
    }

    private void ExecuteInteraction()
    {
        switch (type)
        {
            case InteractionType.Calendar:
                Object.FindAnyObjectByType<SceneController>()?.LoadCalendar();
                break;
            case InteractionType.DaySummary:
                Object.FindAnyObjectByType<SceneController>()?.LoadDaySummary();
                break;
            case InteractionType.LunaDialogue:
                Object.FindAnyObjectByType<DialogueController>()?.ShowDialogue();
                break;
        }
    }
}
