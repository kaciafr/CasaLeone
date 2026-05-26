using DG.Tweening;
using UnityEngine;

public class OptionsMenuAnimator : MonoBehaviour
{
    [SerializeField] private GameObject    optionsMenu;
    [SerializeField] private RectTransform rectTransformOptions;

    [Header("Positions")]
    [SerializeField] private float hiddenX  = 800f;
    [SerializeField] private float visibleX = 0f;
    [SerializeField] private float duration = 0.5f;

    public bool isOpen = false;

    private void Start()
    {
        optionsMenu.SetActive(false);
        rectTransformOptions.anchoredPosition =
            new Vector2(hiddenX, rectTransformOptions.anchoredPosition.y);
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;
        optionsMenu.SetActive(true);
        rectTransformOptions.DOKill();
        rectTransformOptions
            .DOAnchorPosX(visibleX, duration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;
        rectTransformOptions.DOKill();
        rectTransformOptions
            .DOAnchorPosX(hiddenX, duration)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => optionsMenu.SetActive(false));
    }

    /// <summary>Ferme sans vérifier isOpen — pour PauseMenuManager.</summary>
    public void ForceClose()
    {
        if (!isOpen) return;
        isOpen = false;
        rectTransformOptions.DOKill();
        rectTransformOptions
            .DOAnchorPosX(hiddenX, duration)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => optionsMenu.SetActive(false));
    }
}