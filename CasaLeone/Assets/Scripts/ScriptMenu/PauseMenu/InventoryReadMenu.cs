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

    public bool isOpen = false;

    private void Start()
    {
        readMenu.SetActive(false);
        rectTransformInventory.anchoredPosition = 
            new Vector2(hiddenX, rectTransformInventory.anchoredPosition.y);
    }

    /// <summary>Appelé par le bouton Inventaire directement.</summary>
    public void ButtonClick()
    {
        if (isOpen) return;
        isOpen = true;

        readMenu.SetActive(true);
        invRead.OnInventoryOpen(); // slide les boutons

        rectTransformInventory.DOKill();
        rectTransformInventory
            .DOAnchorPosX(visibleX, duration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    /// <summary>
    /// Appelé par PauseMenuManager — n'appelle PAS OnInventoryOpen
    /// car PauseMenuManager gère les boutons lui-même.
    /// </summary>
    public void ButtonClickManaged()
    {
        if (isOpen) return;
        isOpen = true;

        readMenu.SetActive(true);
        // Pas de invRead.OnInventoryOpen() ici

        rectTransformInventory.DOKill();
        rectTransformInventory
            .DOAnchorPosX(visibleX, duration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    /// <summary>Appelé par le bouton fermer de l'inventaire.</summary>
    public void Other()
    {
        if (!isOpen) return;
        isOpen = false;

        invRead.OnInventoryClose(); // remet les boutons

        rectTransformInventory.DOKill();
        rectTransformInventory
            .DOAnchorPosX(hiddenX, duration)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => readMenu.SetActive(false));
    }

    /// <summary>
    /// Appelé par PauseMenuManager — n'appelle PAS OnInventoryClose
    /// car PauseMenuManager gère les boutons lui-même.
    /// </summary>
    public void ForceClose()
    {
        if (!isOpen) return;
        isOpen = false;
        // Pas de invRead.OnInventoryClose() ici

        rectTransformInventory.DOKill();
        rectTransformInventory
            .DOAnchorPosX(hiddenX, duration)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => readMenu.SetActive(false));
    }
}