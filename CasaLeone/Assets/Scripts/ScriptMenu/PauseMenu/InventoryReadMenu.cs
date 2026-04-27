using DG.Tweening;
using UnityEngine;

public class InventoryReadMenu : MonoBehaviour
{
    [SerializeField] private GameObject readMenu;
    [SerializeField] private RectTransform rectTransformInventory;
    [SerializeField] private InvRead invRead;

    [Header("Positions Inventaire")]
    [SerializeField] private float hiddenX = 800f;   
    [SerializeField] private float visibleX = 0f;    
    [SerializeField] private float duration = 0.5f;

    private bool isOpen = false;

    private void Start()
    {
        readMenu.SetActive(false);
        rectTransformInventory.anchoredPosition = 
            new Vector2(hiddenX, rectTransformInventory.anchoredPosition.y);
    }


    public void ButtonClick()
    {
        if (isOpen) return;
        isOpen = true;

        readMenu.SetActive(true);
        invRead.OnInventoryOpen(); 

        rectTransformInventory.DOKill();
        rectTransformInventory
            .DOAnchorPosX(visibleX, duration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

 
    public void Other()
    {
        if (!isOpen) return;
        isOpen = false;

        invRead.OnInventoryClose(); 

        rectTransformInventory.DOKill();
        rectTransformInventory
            .DOAnchorPosX(hiddenX, duration)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => readMenu.SetActive(false));
    }
}