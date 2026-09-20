using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class DragCup : MonoBehaviour
    {
        [SerializeField] private LayerMask customerDropLayer;
        [SerializeField] private CreateDrink createDrink;

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
            if (!GlobalFields.Instance.MouseInteractable)
            {
                return;
            }

            // Can't serve an empty/unfinished cup.
            if (createDrink.CurrentDrink == null)
            {
                return;
            }

            _initialPosition = transform.position;
            _dragOffset = transform.position - GetMouseWorldPosition();
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

            var filter = new ContactFilter2D();
            filter.SetLayerMask(customerDropLayer);
            filter.useTriggers = true;

            var results = new List<Collider2D>();
            Physics2D.OverlapPoint(transform.position, filter, results);

            Debug.Log($"[DragCup] Drop at {transform.position}: found {results.Count} collider(s) on layer mask {customerDropLayer.value}");

            CustomerDropArea dropArea = null;
            foreach (var col in results)
            {
                Debug.Log($"[DragCup] Checking collider: {col.gameObject.name}");
                if (col.TryGetComponent(out dropArea))
                {
                    Debug.Log($"[DragCup] Found CustomerDropArea on {col.gameObject.name}");
                    break;
                }
            }

            if (dropArea == null)
            {
                Debug.Log("[DragCup] No CustomerDropArea found at drop position.");
            }

            if (dropArea != null && createDrink.CurrentDrink != null)
            {
                dropArea.ReceiveCup(createDrink.CurrentDrink.Value);
            }

            // The cup stays at its home position either way - only the
            // drink data travels to the customer; the sprite itself fades
            // out separately via CreateDrink.RemoveDrink().
            transform.position = _initialPosition;
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