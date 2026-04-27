using DG.Tweening;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvRead : MonoBehaviour
{
    
    public static InvRead Instance  { get; private set; }
    
    [Header("References")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject infoItem;
    [SerializeField] private TextMeshProUGUI txtItem;
    [SerializeField] private Image iconItem;
    [SerializeField] private GameObject inventoryObject ;

    [Header("Info Panel Positions")]
    [SerializeField] private float infoHiddenX = 600f;
    [SerializeField] private float infoVisibleX = 200f;
    [SerializeField] private float infoDuration = 0.4f;

    [Header("Panel Boutons")]
    [SerializeField] private RectTransform pannelButtonSlide;
    [SerializeField] private float buttonsDuration = 0.6f;
    [SerializeField] private float buttonsSlideOffset = 300f;

    private RectTransform infoRect;
    private float buttonsOriginalX;
    private ItemData currentItem = null;
    private bool isInfoVisible = false;

    private void Awake()
    {
        infoRect = infoItem.GetComponent<RectTransform>();
        buttonsOriginalX = pannelButtonSlide.anchoredPosition.x;
        infoHiddenX = infoRect.anchoredPosition.x; 

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; 
    }


    private void Start()
    {
        infoItem.SetActive(false);
        inventoryObject.SetActive(false);
    }



    public void OnInventoryOpen()
    {
        SlideButtonsOut();
    }

    public void OnInventoryClose()
    {
        SlideButtonsIn();
        HideInfo(null);
    }

    public  void SlideButtonsOut()
    {
        pannelButtonSlide.DOKill();
        pannelButtonSlide
            .DOAnchorPosX(buttonsOriginalX - buttonsSlideOffset, buttonsDuration)
            .SetDelay(0.2f)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true);
        inventoryObject.SetActive(true);
        
        
    }

    public  void SlideButtonsIn()
    {
        pannelButtonSlide.DOKill();
        pannelButtonSlide
            .DOAnchorPosX(buttonsOriginalX, buttonsDuration)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true);
        
        inventoryObject.SetActive(false);
        
        
        
        
    }

    public void OnClick(ItemData obj)
    {
        if (isInfoVisible && currentItem == obj)
        {
            HideInfo(null);
            return;
        }

        if (isInfoVisible)
        {
            HideInfo(() => ShowInfo(obj));
        }
        else
        {
            ShowInfo(obj);
        }
    }

    private void ShowInfo(ItemData obj)
    {
        currentItem = obj;
        isInfoVisible = true;

        iconItem.sprite = obj.icon;
        txtItem.text = obj.description;

        infoItem.SetActive(true);

        infoRect.anchoredPosition = new Vector2(infoHiddenX, infoRect.anchoredPosition.y);
        infoRect.DOKill();
        infoRect
            .DOAnchorPosX(infoVisibleX, infoDuration)
            .SetEase(Ease.OutCubic)
            .SetUpdate(true);
    }

    private void HideInfo(TweenCallback onComplete)
    {
        isInfoVisible = false;
        currentItem = null;

        infoRect.DOKill();
        infoRect
            .DOAnchorPosX(infoHiddenX, infoDuration)
            .SetEase(Ease.InCubic)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                infoItem.SetActive(false);
                onComplete?.Invoke();
            });
    }

    public void AddInventoryItem(ItemData obj)
    {
        GameObject go = Instantiate(itemPrefab, container);
        itemSlot slot = go.GetComponent<itemSlot>();
        slot.UpdateVisuel(obj, this);
    }
}
