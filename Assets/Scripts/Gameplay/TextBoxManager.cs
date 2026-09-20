using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextBoxManager : MonoBehaviour
{
    
    public static TextBoxManager Instance { get; private set; }
    [SerializeField] private TextMeshPro labelText;
    [SerializeField] private int maxCharsPerLine = 50;

    private Tween currentTween;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Clear duplicate
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        if (labelText == null)
            labelText = GetComponentInChildren<TextMeshPro>();
    }

    // private void OnDisable()
    // {
    //     currentTween?.Kill();
    // }

    public void SetDisabled(bool disabled)
    {
        if (disabled)
        {
            gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
    }

    public async Task AnimateFadeIn(string newText, float duration = 0.5f)
    {
        // 1. Kill any active tween
        currentTween?.Kill();

        // 2. Format and assign the text
        string formattedText = InsertLineBreaks(newText, maxCharsPerLine);
        labelText.text = formattedText;

        // 3. Start from 0 alpha
        labelText.alpha = 0f;

        // 4. Tween labelText.alpha directly instead of using DOFade
        currentTween = DOTween.To(
            () => labelText.alpha, 
            x => labelText.alpha = x, 
            1f, 
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
