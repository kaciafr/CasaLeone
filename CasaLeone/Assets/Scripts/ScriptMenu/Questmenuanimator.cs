using DG.Tweening;
using UnityEngine;

public class QuestMenuAnimator : MonoBehaviour
{
    [SerializeField] private GameObject      questMenu;
    [SerializeField] private RectTransform   rectTransformQuest;

    [Header("Positions")]
    [SerializeField] private float hiddenX  = 800f;
    [SerializeField] private float visibleX = 0f;
    [SerializeField] private float duration = 0.5f;

    private bool isOpen = false;

    private void Start()
    {
        questMenu.SetActive(false);
        rectTransformQuest.anchoredPosition =
            new Vector2(hiddenX, rectTransformQuest.anchoredPosition.y);
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;

        questMenu.SetActive(true);

        rectTransformQuest.DOKill();
        rectTransformQuest
            .DOAnchorPosX(visibleX, duration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;

        rectTransformQuest.DOKill();
        rectTransformQuest
            .DOAnchorPosX(hiddenX, duration)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => questMenu.SetActive(false));
    }
}