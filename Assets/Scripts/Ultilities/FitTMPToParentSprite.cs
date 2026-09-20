using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class FitTMPToParentSprite : MonoBehaviour
{
    [Tooltip("Padding in Unity world units inside the sprite border")]
    [SerializeField] private Vector2 padding = new Vector2(0.2f, 0.2f);

    private TextMeshPro tmp;
    private SpriteRenderer parentRenderer;

    private void Awake()
    {
        FitToSprite();
    }

    private void OnValidate()
    {
        FitToSprite();
    }

    [ContextMenu("Fit To Sprite")]
    public void FitToSprite()
    {
        if (tmp == null) tmp = GetComponent<TextMeshPro>();
        if (parentRenderer == null) parentRenderer = GetComponentInParent<SpriteRenderer>();

        if (parentRenderer == null || parentRenderer.sprite == null) return;

        // 1. Calculate local dimensions based on draw mode
        Vector2 localSize;
        if (parentRenderer.drawMode == SpriteDrawMode.Simple)
        {
            Sprite sprite = parentRenderer.sprite;
            localSize = sprite.rect.size / sprite.pixelsPerUnit;
        }
        else
        {
            // Sliced, Tiled, or Adaptive modes store size directly
            localSize = parentRenderer.size;
        }

        // 2. Update RectTransform
        RectTransform rt = tmp.rectTransform;
        rt.localPosition = new Vector3(0.6f, -1.1f, 0);
        rt.sizeDelta = new Vector2(
            Mathf.Max(0.01f, localSize.x - padding.x),
            Mathf.Max(0.01f, localSize.y - padding.y)
        );
        
    }
}