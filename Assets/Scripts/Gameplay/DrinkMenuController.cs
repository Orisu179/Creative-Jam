using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DrinkMenuController : MonoBehaviour
{
    [Header("Menu Root")]
    [Tooltip("The parent panel containing the whole menu (background + pages). Needs a CanvasGroup component.")]
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private float fadeDuration = 0.3f;

    [Header("Pages")]
    [Tooltip("Drag in all 6 page GameObjects, in order.")]
    [SerializeField] private List<GameObject> pages = new List<GameObject>();

    private CanvasGroup menuCanvasGroup;
    private int currentPageIndex;
    private bool isOpen;

    private void Awake()
    {
        menuCanvasGroup = menuRoot.GetComponent<CanvasGroup>();
        if (menuCanvasGroup == null)
        {
            menuCanvasGroup = menuRoot.AddComponent<CanvasGroup>();
        }

        // Start closed and on page 0, with no animation.
        SetPageInstant(0);
        menuCanvasGroup.alpha = 0f;
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
        menuRoot.SetActive(false);
        isOpen = false;
    }

    // Wire this to the "open/close menu" button's OnClick().
    public void ToggleMenu()
    {
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

        menuRoot.SetActive(true);
        SetPageInstant(0);

        menuCanvasGroup.DOKill();
        menuCanvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.OutQuad);
        menuCanvasGroup.interactable = true;
        menuCanvasGroup.blocksRaycasts = true;
        isOpen = true;
    }

    public void CloseMenu()
    {
        if (!isOpen) return;

        menuCanvasGroup.DOKill();
        menuCanvasGroup.DOFade(0f, fadeDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => menuRoot.SetActive(false));

        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
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