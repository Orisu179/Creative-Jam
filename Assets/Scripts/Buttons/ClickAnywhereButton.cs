using UnityEngine;

public class ClickAnywhereButton : MonoBehaviour
{
    [SerializeField] private TextBoxManager textBoxManager;

    private void Awake()
    {
        if (textBoxManager == null)
        {
            textBoxManager = TextBoxManager.Instance;
        }
    }
    public void CompleteDialogue()
    {
        Debug.Log("ClickAnywhereButton: OnMouseDown called");
        textBoxManager.CompleteDialogue();
    }
}