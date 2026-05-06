using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{
    public UnityEvent onClick;

    void OnMouseDown()
    {
        if (onClick != null)
        {
            onClick.Invoke();
        }
    }
}
