using DG.Tweening;
using UnityEngine;

public class InventoryReadMenu : MonoBehaviour
{
    [SerializeField] private GameObject readMenu;
    [SerializeField] private RectTransform _rectTransformbutton;

    private Vector2 _startButtonPos;

    private void Awake()
    {
        _startButtonPos = _rectTransformbutton.anchoredPosition;
    }

    private void Start()
    {
        readMenu.SetActive(false);
    }

    public void ButtonClick()
    {
        readMenu.SetActive(true);

        _rectTransformbutton.DOKill();
        _rectTransformbutton.DOAnchorPosX(-200f, 0.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true); 
    }

    public void Other()
    {
        _rectTransformbutton.DOKill();
        _rectTransformbutton.DOAnchorPosX(_startButtonPos.x, 0.5f)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => readMenu.SetActive(false)); 
    }
}