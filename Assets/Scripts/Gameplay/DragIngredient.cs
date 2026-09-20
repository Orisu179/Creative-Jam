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
        [Tooltip("If set, the dragged visual uses this sprite instead of the source's own sprite. Leave empty to drag the same sprite.")]
        [SerializeField] private Sprite dragSprite;

        [Header("Infinite Source (optional)")]
        [Tooltip("If true, this object (e.g. a bowl of ice) never moves. A temporary visual clone is spawned and dragged instead, then destroyed when the drag ends.")]
        [SerializeField] private bool spawnDragClone;

        private Vector3 _initialPosition;
        private Vector3 _dragOffset;
        private Camera _mainCamera;
        private bool _isDragging;

        private SpriteRenderer _spriteRenderer;
        private Sprite _defaultSprite;

        private Transform _dragVisual;
        private SpriteRenderer _dragVisualRenderer;

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

            var mouseWorldPos = GetMouseWorldPosition();
            _dragOffset = transform.position - mouseWorldPos;
            _isDragging = true;

            if (spawnDragClone)
            {
                SpawnDragVisual();
            }
            else
            {
                _initialPosition = transform.position;

                if (dragSprite != null)
                {
                    _spriteRenderer.sprite = dragSprite;
                }
            }
        }

        private void OnMouseDrag()
        {
            if (!_isDragging) return;

            var newPosition = GetMouseWorldPosition() + _dragOffset;

            if (spawnDragClone)
            {
                _dragVisual.position = newPosition;
            }
            else
            {
                transform.position = newPosition;
            }
        }

        private void OnMouseUp()
        {
            if (!_isDragging) return;
            _isDragging = false;

            var dropPosition = spawnDragClone ? _dragVisual.position : transform.position;

            // Check if dropped within the boundaries of a drop zone.
            // Use an explicit ContactFilter2D so this doesn't silently
            // depend on the project's global "Queries Hit Triggers"
            // setting (a common source of inconsistent drop detection).
            var filter = new ContactFilter2D();
            filter.SetLayerMask(dropAreaLayer);
            filter.useTriggers = true;

            var results = new System.Collections.Generic.List<Collider2D>();
            Physics2D.OverlapPoint(dropPosition, filter, results);

            MixingCupArea dropArea = null;
            foreach (var col in results)
            {
                if (col.TryGetComponent(out dropArea))
                {
                    break;
                }
            }

            bool droppedSuccessfully = dropArea != null;

            if (droppedSuccessfully)
            {
                ingredient.dropDragEvent.Post(gameObject);
                dropArea.ReceiveDrop(ingredient.IngredientType);
            }

            if (spawnDragClone)
            {
                // The source (bowl) never moved. The clone was only ever a
                // visual stand-in, so clean it up whether the drop was
                // valid or not - there's nothing to "return".
                Destroy(_dragVisual.gameObject);
                _dragVisual = null;
                _dragVisualRenderer = null;
            }
            else
            {
                if (droppedSuccessfully || returnOnInvalidDrop)
                {
                    transform.position = _initialPosition;
                }

                if (dragSprite != null)
                {
                    _spriteRenderer.sprite = _defaultSprite;
                }
            }
        }

        private void SpawnDragVisual()
        {
            var visualObject = new GameObject($"{gameObject.name}_DragVisual");
            _dragVisualRenderer = visualObject.AddComponent<SpriteRenderer>();
            _dragVisualRenderer.sprite = dragSprite != null ? dragSprite : _defaultSprite;
            _dragVisualRenderer.sortingLayerID = _spriteRenderer.sortingLayerID;
            _dragVisualRenderer.sortingOrder = _spriteRenderer.sortingOrder + 1;

            _dragVisual = visualObject.transform;
            _dragVisual.position = transform.position;
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