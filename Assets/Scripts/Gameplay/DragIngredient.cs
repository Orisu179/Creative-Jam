using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class DragIngredient : MonoBehaviour
    {
        [SerializeField] private LayerMask dropAreaLayer;
        [SerializeField] private bool returnOnInvalidDrop = true;
        [SerializeField] private Ingredient ingredient;

        private Vector3 _initialPosition;
        private Vector3 _dragOffset;
        private Camera _mainCamera;
        private bool _isDragging;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnMouseDown()
        {
            _initialPosition = transform.position;
            var mouseWorldPos = GetMouseWorldPosition();
            _dragOffset = transform.position - mouseWorldPos;
            _isDragging = true;
        }

        private void OnMouseDrag()
        {
            if (!_isDragging) return;
            transform.position = GetMouseWorldPosition() + _dragOffset;
        }

        private void OnMouseUp()
        {
            if (!_isDragging) return;
            _isDragging = false;

            // Check if dropped within the boundaries of a drop zone
            var hit = Physics2D.OverlapPoint(transform.position, dropAreaLayer);
        
            if (hit != null && hit.TryGetComponent<MixingCupArea>(out var dropArea))
            {
                dropArea.ReceiveDrop(ingredient);
                transform.position = _initialPosition;
            }
            else if (returnOnInvalidDrop)
            {
                transform.position = _initialPosition;
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = -_mainCamera.transform.position.z;
            var worldPos = _mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;
            return worldPos;
        }
    }
}