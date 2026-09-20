using UnityEngine;
using UnityEngine.Events;

// Attach to any GameObject with a Collider/Collider2D that should act as a
// clickable button via OnMouseDown. Wire OnClick in the Inspector exactly
// like a UI Button's OnClick() list.
[RequireComponent(typeof(Collider2D))]
public class MouseDownButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onClick;

    private void OnMouseDown()
    {
        Debug.Log("clicked");
        onClick?.Invoke();
    }
}