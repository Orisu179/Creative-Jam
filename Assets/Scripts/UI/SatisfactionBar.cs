using UnityEngine;
using DG.Tweening; // Required for DOTween
using TMPro; // Required for TextMeshProUGUI

public class SatisfactionBar : MonoBehaviour
{
	[Header("Sprite & UI References")]
	[SerializeField] private SpriteRenderer barSpriteRenderer;
	[SerializeField] private Transform pointerTransform;
	[SerializeField] private TMP_Text scoreText; // Reference to your text display

	[Header("Score Settings")]
	[SerializeField] private float minScore = -10f;
	[SerializeField] private float maxScore = 10f;

	[Header("Layout Settings")]
	[Range(0f, 0.49f)]
	[SerializeField] private float sideMargin = 0.05f; // 5% margin on each side

	[Header("Animation Settings")]
	[SerializeField] private float moveDuration = 0.3f;
	[SerializeField] private Ease moveEase = Ease.OutQuad;

	[Header("Text Formatting")]
	[SerializeField] private string textFormat = "F1"; // "F0" for integers, "F1" for 1 decimal place, etc.

	private float currentScore = 0f;
	private Tween currentTween;

	void Start()
	{
		SetScore(currentScore, false); // Instantly set position on start
	}

	public void SetScore(float score, bool animate = true)
	{
		// Clamp score between -10 and +10
		currentScore = Mathf.Clamp(score, minScore, maxScore);

		// Update the text display
		UpdateScoreText();

		// Normalize score to a 0 to 1 range
		float normalizedValue = Mathf.InverseLerp(minScore, maxScore, currentScore);

		if (barSpriteRenderer != null && pointerTransform != null && barSpriteRenderer.sprite != null)
		{
			// Calculate the total local width of the sprite in world units
			float spriteWidth = barSpriteRenderer.sprite.rect.width / barSpriteRenderer.sprite.pixelsPerUnit;

			// Reduce the active tracking span by the side margins
			float usableWidth = spriteWidth * (1f - (sideMargin * 2f));

			// Map the normalized value within the padded usable width bounds
			float targetX = (normalizedValue - 0.5f) * usableWidth;

			// Kill any active tween to prevent overlapping conflicts
			currentTween?.Kill();

			if (animate)
			{
				// Smoothly animate local position using DOTween
				currentTween = pointerTransform.DOLocalMoveX(targetX, moveDuration).SetEase(moveEase);
			}
			else
			{
				// Snap immediately without animation
				Vector3 localPos = pointerTransform.localPosition;
				localPos.x = targetX;
				pointerTransform.localPosition = localPos;
			}
		}
	}

	private void UpdateScoreText()
	{
		if (scoreText != null)
		{
			// Format the score (e.g., "-5.2" or "10")
			scoreText.text = currentScore.ToString(textFormat);
		}
	}
}