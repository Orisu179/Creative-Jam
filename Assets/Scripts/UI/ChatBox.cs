using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
	[SerializeField] private TMP_Text labelText;
	[SerializeField] private int maxCharsPerLine = 50;

	private Tween currentTween;

	private void Awake()
	{
		if (labelText == null)
			labelText = GetComponentInChildren<TMP_Text>();

		if (labelText == null)
			throw new InvalidOperationException("ChatBox requires a TextMeshPro or TextMeshProUGUI component on this object or a child.");
	}

	// private void OnDisable()
	// {
	//     currentTween?.Kill();
	// }

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	public async Task SetDisabled(bool disabled, float fadeDuration = 0.5f)
	{
		if (disabled)
		{
			// 1. Fade out the label text
			await AnimateFade(0f, fadeDuration);
			// 2. Fade out the sprite
			currentTween = GetComponent<SpriteRenderer>().DOFade(0f, fadeDuration);
			await currentTween.AsyncWaitForCompletion();
		}
		else
		{
			labelText.alpha = 0f;
			currentTween = GetComponent<SpriteRenderer>().DOFade(1f, fadeDuration);
			await currentTween.AsyncWaitForCompletion();
			await AnimateFade(1f, fadeDuration);
		}
	}

	public void SetDialogue(string newText)
	{
		labelText.text = InsertLineBreaks(newText, maxCharsPerLine);
	}

	public async Task AnimateFade(float toAlpha, float duration = 0.5f)
	{
		// 1. Kill any active tween
		currentTween?.Kill();

		// 2. Tween labelText.alpha directly instead of using DOFade
		currentTween = DOTween.To(
			() => labelText.alpha,
			x => labelText.alpha = x,
			toAlpha,
			duration
		).SetEase(Ease.OutQuad);

		await currentTween.AsyncWaitForCompletion();
	}

	private string InsertLineBreaks(string text, int interval)
	{
		if (string.IsNullOrEmpty(text) || text.Length <= interval)
			return text;

		var words = text.Split(' ');
		var sb = new StringBuilder();
		var lineLength = 0;

		foreach (var word in words)
		{
			// If adding this word exceeds the line limit, wrap to the next line
			if (lineLength + word.Length > interval && lineLength > 0)
			{
				sb.Append('\n');
				lineLength = 0;
			}
			else if (lineLength > 0)
			{
				sb.Append(' ');
				lineLength++;
			}

			sb.Append(word);
			lineLength += word.Length;
		}

		return sb.ToString();
	}
}
