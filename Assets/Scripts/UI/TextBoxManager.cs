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
    [SerializeField] private ChatBox chatBox;
    [SerializeField] private GameObject clickAnywhereButton;
    public Action OnDialogueComplete { get; set; }
    private string _pendingDialogue;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Clear duplicate
            return;
        }

        Instance = this;
    }

    public async Task SetDisabled(bool disabled, float fadeDuration = 0.5f)
    {
        if (disabled)
        {
            chatBox.SetActive(false);
            await chatBox.SetDisabled(true, fadeDuration);
            clickAnywhereButton.gameObject.SetActive(false);
        }
        else
        {
            chatBox.SetActive(true);
            chatBox.SetDialogue(_pendingDialogue);
            await chatBox.SetDisabled(false, fadeDuration);
            clickAnywhereButton.gameObject.SetActive(true);
        }
    }

    public void SetDialogue(string newText, Action onComplete)
    {
        OnDialogueComplete = onComplete;
        _pendingDialogue = newText;
    }

    public async Task CompleteDialogue()
    {
        Debug.Log("TextBoxManager: CompleteDialogue called");
        // 1. Deactivate the dialogue box
        await SetDisabled(true);

        // 2. Invoke the callback if it's set
        OnDialogueComplete?.Invoke();
    }
}
