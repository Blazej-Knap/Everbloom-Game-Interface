using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.UI;
#endif

public class GlobalUIFixer : MonoBehaviour
{
    [Header("Hover Settings")]
    public Color hoverTintColor = new Color(1f, 1f, 0.8f, 1f); 
    public float hoverScale = 1.05f;
    public Vector2 pressedOffset = new Vector2(0, -6f);

    private void Awake()
    {
        ApplyAllFixes();
    }

    public void ApplyAllFixes()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
        FixEventSystem();
        FixInteractionAndHover();
    }

    private void FixEventSystem()
    {
        EventSystem es = Object.FindFirstObjectByType<EventSystem>();
        if (es == null)
        {
            GameObject esObj = new GameObject("EventSystem (Auto-Generated)");
            es = esObj.AddComponent<EventSystem>();
            #if ENABLE_INPUT_SYSTEM
            esObj.AddComponent<InputSystemUIInputModule>();
            #else
            esObj.AddComponent<StandaloneInputModule>();
            #endif
        }
    }

    public void FixInteractionAndHover()
    {
        TMP_Text[] allTexts = Object.FindObjectsByType<TMP_Text>(FindObjectsSortMode.None);
        foreach (var txt in allTexts)
        {
            if (txt.GetComponentInParent<Selectable>() == null)
                txt.raycastTarget = false;
        }

        Button[] buttons = Object.FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (var btn in buttons)
        {
            var effect = btn.gameObject.GetComponent<ButtonHoverEffect>();
            if (effect == null)
            {
                effect = btn.gameObject.AddComponent<ButtonHoverEffect>();
            }
            effect.hoverScale = hoverScale;
            effect.hoverColor = hoverTintColor;
            effect.pressedOffset = pressedOffset;
        }
    }
    }
