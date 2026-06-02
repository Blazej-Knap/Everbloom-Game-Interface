using UnityEngine;
using UnityEngine.EventSystems;
using Kinnly;

public class InteractionBridge : MonoBehaviour, IPointerClickHandler, IInteractable, IPointerEnterHandler, IPointerExitHandler
{
    public enum InteractionType { Calendar, DaySummary, LunaDialogue }
    public InteractionType type;

    private GameObject[] _outlineObjects;
    private bool _isHovered = false;

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
        if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject()) // Prevent double fire if clicked through UI
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        ShowHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideHover();
    }

    public void OnMouseEnter()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        ShowHover();
    }

    public void OnMouseExit()
    {
        HideHover();
    }

    private void ShowHover()
    {
        _isHovered = true;
        CreateOutline();
    }

    private void HideHover()
    {
        _isHovered = false;
        DestroyOutline();
    }

    private void CreateOutline()
    {
        if (_outlineObjects != null) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Shader flatColorShader = Shader.Find("Custom/FlatColorSprite");
        Material outlineMat;
        if (flatColorShader != null)
        {
            outlineMat = new Material(flatColorShader);
            outlineMat.SetColor("_Color", new Color(1f, 1f, 1f, 0.8f)); // Clean white outline
        }
        else
        {
            outlineMat = new Material(Shader.Find("Sprites/Default"));
            outlineMat.color = new Color(1f, 1f, 1f, 0.8f);
        }

        _outlineObjects = new GameObject[4];
        float pixelSize = 1f / 16f; // 16 PPU grid
        Vector3[] offsets = new Vector3[]
        {
            new Vector3(0, pixelSize, 0),
            new Vector3(0, -pixelSize, 0),
            new Vector3(pixelSize, 0, 0),
            new Vector3(-pixelSize, 0, 0)
        };

        for (int i = 0; i < 4; i++)
        {
            GameObject go = new GameObject($"Outline_{i}");
            go.transform.SetParent(transform, false);
            go.transform.localPosition = offsets[i];
            go.transform.localScale = Vector3.one;

            SpriteRenderer outlineSr = go.AddComponent<SpriteRenderer>();
            outlineSr.sprite = sr.sprite;
            outlineSr.material = outlineMat;
            outlineSr.sortingLayerID = sr.sortingLayerID;
            outlineSr.sortingOrder = sr.sortingOrder - 1; // Render directly behind original sprite
            outlineSr.flipX = sr.flipX;
            outlineSr.flipY = sr.flipY;

            _outlineObjects[i] = go;
        }
    }

    private void DestroyOutline()
    {
        if (_outlineObjects == null) return;
        foreach (var go in _outlineObjects)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
        _outlineObjects = null;
    }

    private void Update()
    {
        if (_isHovered && _outlineObjects != null)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                for (int i = 0; i < _outlineObjects.Length; i++)
                {
                    if (_outlineObjects[i] != null)
                    {
                        SpriteRenderer outlineSr = _outlineObjects[i].GetComponent<SpriteRenderer>();
                        if (outlineSr != null)
                        {
                            outlineSr.sprite = sr.sprite;
                            outlineSr.flipX = sr.flipX;
                            outlineSr.flipY = sr.flipY;
                        }
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        DestroyOutline();
    }
}
