using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// DrinkMenu itself stays active in the scene at all times now.
// GameManager controls access via EnableButton()/CloseAndDisable() on
// CreateDrinkState.Enter()/Exit() instead of toggling this whole GameObject.
public class DrinkMenuController : MonoBehaviour
{
    [Header("Content")]
    [Tooltip("The container holding the pages + nav arrows. Shown/hidden on open/close.")]
    [SerializeField] private GameObject menuContent;

    [Header("Open/Close Button")]
    [Tooltip("The Button component on the open/close menu object. Disabling this script stops OnClick from firing at all.")]
    [SerializeField] private Button openCloseButton;

    [Header("Pages")]
    [Tooltip("Drag in all page GameObjects, in order.")]
    [SerializeField] private List<GameObject> pages = new List<GameObject>();

    private int currentPageIndex;
    private bool isOpen;

    private void Awake()
    {
        SetPageInstant(0);
        menuContent.SetActive(false);
        DisableButton();
        isOpen = false;
    }

    // Call on CreateDrinkState.Enter().
    public void EnableButton()
    {
        openCloseButton.enabled = true;
    }

    // Call on CreateDrinkState.Exit(): hides the menu AND locks the button.
    public void CloseAndDisable()
    {
        
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayMenuButton);
        CloseMenu();
        SetPageInstant(0);
        DisableButton();
    }

    private void DisableButton()
    {
        openCloseButton.enabled = false;
    }

    // Wired to the open/close button's OnClick.
    public void ToggleMenu()
    {
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayMenuButton);
        if (isOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        if (isOpen) return;
        menuContent.SetActive(true);
        isOpen = true;
    }

    public void CloseMenu()
    {
        if (!isOpen) return;
        menuContent.SetActive(false);
        isOpen = false;
    }

    // Wire these to Next/Previous page buttons.
    public void NextPage()
    {
        if (currentPageIndex < pages.Count - 1)
        {
            GoToPage(currentPageIndex + 1);
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            GoToPage(currentPageIndex - 1);
        }
    }

    public void GoToPage(int index)
    {
        SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayPageFlip);
        if (index < 0 || index >= pages.Count || pages.Count == 0) return;

        if (pages[currentPageIndex] != null)
        {
            pages[currentPageIndex].SetActive(false);
        }

        currentPageIndex = index;
        pages[currentPageIndex].SetActive(true);
    }

    private void SetPageInstant(int index)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i] != null)
            {
                pages[i].SetActive(i == index);
            }
        }
        currentPageIndex = Mathf.Clamp(index, 0, Mathf.Max(0, pages.Count - 1));
    }
}