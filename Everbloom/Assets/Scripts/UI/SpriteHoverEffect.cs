using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f;
    public Color hoverColor = new Color(1f, 1f, 0.8f, 1f);

    private Vector3 _originalScale;
    private Color _originalColor;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _originalScale = transform.localScale;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null) _originalColor = _spriteRenderer.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _originalScale * hoverScale;
        if (_spriteRenderer != null) _spriteRenderer.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _originalScale;
        if (_spriteRenderer != null) _spriteRenderer.color = _originalColor;
    }

    void OnDisable()
    {
        transform.localScale = _originalScale;
        if (_spriteRenderer != null) _spriteRenderer.color = _originalColor;
    }
}
