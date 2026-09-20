using System.Collections;
using UnityEngine;

public class ClickAnywhereButton : MonoBehaviour
{
    [SerializeField] private TextBoxManager textBoxManager;
    [SerializeField] private float cooldownDuration;
    private bool isCooldown = false;

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
        if (!isCooldown)
        {
            StartCoroutine(CooldownRoutine());
        }
        textBoxManager.CompleteDialogue();
    }
    private IEnumerator CooldownRoutine()
    {
        isCooldown = true;
        Debug.Log("Action executed!");

        yield return new WaitForSeconds(cooldownDuration);

        isCooldown = false;
    }
}