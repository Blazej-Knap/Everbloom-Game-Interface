using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToolbarSelectionController : MonoBehaviour
{
    public Transform gridTransform;
    public Color selectedColor = new Color(1f, 0.85f, 0f, 1f); // Highlight Yellow
    public Color normalColor = Color.white;

    private List<Image> slots = new List<Image>();
    private int currentIndex = -1;

    void Start()
    {
        if (gridTransform == null) gridTransform = transform.Find("Grid");
        
        if (gridTransform != null)
        {
            foreach (Transform child in gridTransform)
            {
                Image img = child.GetComponent<Image>();
                if (img != null)
                {
                    slots.Add(img);
                }
            }
        }

        // Default selection to slot 1
        SelectSlot(0);
    }

    void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectSlot(9);
        }
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return;

        // Reset previous
        if (currentIndex != -1 && currentIndex < slots.Count)
        {
            slots[currentIndex].color = normalColor;
        }

        // Set new
        currentIndex = index;
        slots[currentIndex].color = selectedColor;
        
        Debug.Log("Selected slot " + (index + 1));
    }
}
