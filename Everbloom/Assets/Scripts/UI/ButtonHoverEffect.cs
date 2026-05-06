using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public float hoverScale = 1.05f;
    public Color hoverColor = new Color(1f, 1f, 0.8f, 1f);
    public Vector2 pressedOffset = new Vector2(0, -6f);
    
    private Vector3 _originalScale;
    private Color _originalColor;
    private Image _targetImage;
    
    private List<RectTransform> _childrenToMove = new List<RectTransform>();
    private List<Vector2> _originalPositions = new List<Vector2>();
    
    private bool _isHovered = false;
    private bool _isPressed = false;
    private bool _initialized = false;
    private Selectable _selectable;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_initialized) return;
        
        _selectable = GetComponent<Selectable>();
        _originalScale = transform.localScale;
        _targetImage = GetComponent<Image>();
        if (_targetImage != null) _originalColor = _targetImage.color;

        // Znajdź wszystkie RectTransformy wewnątrz (rekurencyjnie), wykluczając sam przycisk
        RectTransform[] allChildren = GetComponentsInChildren<RectTransform>(true);
        foreach (var rt in allChildren)
        {
            if (rt.transform == transform) continue;
            
            _childrenToMove.Add(rt);
            _originalPositions.Add(rt.anchoredPosition);
        }
        
        _initialized = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_selectable != null && !_selectable.interactable) return;
        _isHovered = true;
        UpdateVisuals();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        UpdateVisuals();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_selectable != null && !_selectable.interactable) return;
        _isPressed = true;
        UpdateVisuals();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (!_initialized) Initialize();

        bool canShowEffect = _selectable == null || _selectable.interactable;

        // Skala przy hoverze (tylko gdy nie wciśnięty)
        transform.localScale = (canShowEffect && _isHovered && !_isPressed) ? _originalScale * hoverScale : _originalScale;

        // Kolor tła przy hoverze
        if (_targetImage != null)
        {
            _targetImage.color = (canShowEffect && _isHovered && !_isPressed) ? hoverColor : _originalColor;
        }

        // Efekt "wciskania" zawartości (tekstów, ikon)
        for (int i = 0; i < _childrenToMove.Count; i++)
        {
            if (_childrenToMove[i] != null)
            {
                _childrenToMove[i].anchoredPosition = (canShowEffect && _isPressed) ? _originalPositions[i] + pressedOffset : _originalPositions[i];
            }
        }
    }

    private void OnDisable()
    {
        if (!_initialized) return;
        transform.localScale = _originalScale;
        if (_targetImage != null) _targetImage.color = _originalColor;
        
        for (int i = 0; i < _childrenToMove.Count; i++)
        {
            if (_childrenToMove[i] != null)
                _childrenToMove[i].anchoredPosition = _originalPositions[i];
        }
        
        _isHovered = false;
        _isPressed = false;
    }
}
