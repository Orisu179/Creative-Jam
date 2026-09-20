using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class DragIngredient : MonoBehaviour
    {
        [SerializeField] private LayerMask dropAreaLayer;
        [SerializeField] private bool returnOnInvalidDrop = true;
        [SerializeField] private IngredientObject ingredient;

        [Header("Drag Appearance (optional)")]
        [Tooltip("If set, the sprite swaps to this while being dragged, and back to the original sprite on release. Leave empty to keep the same sprite throughout.")]
        [SerializeField] private Sprite dragSprite;

        private Vector3 _initialPosition;
        private Vector3 _dragOffset;
        private Camera _mainCamera;
        private bool _isDragging;

        private SpriteRenderer _spriteRenderer;
        private Sprite _defaultSprite;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultSprite = _spriteRenderer.sprite;
        }

        private void OnMouseDown()
        {
            if (!GlobalFields.Instance.MouseInteractable)
            {
                return;
            }
            ingredient.playDragEvent.Post(gameObject);
            _initialPosition = transform.position;
            var mouseWorldPos = GetMouseWorldPosition();
            _dragOffset = transform.position - mouseWorldPos;
            _isDragging = true;

            if (dragSprite != null)
            {
                _spriteRenderer.sprite = dragSprite;
            }
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
                ingredient.dropDragEvent.Post(gameObject);
                dropArea.ReceiveDrop(ingredient.IngredientType);
                transform.position = _initialPosition;
            }
            else if (returnOnInvalidDrop)
            {
                transform.position = _initialPosition;
            }

            if (dragSprite != null)
            {
                _spriteRenderer.sprite = _defaultSprite;
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