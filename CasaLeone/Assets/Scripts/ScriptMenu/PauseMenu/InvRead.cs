using DG.Tweening;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvRead : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private GameObject itemPrefab;
	[SerializeField] private Transform container;
	[SerializeField] private GameObject infoItem;
	[SerializeField] private TextMeshProUGUI txtItem;
	[SerializeField] private Image iconItem;
	[SerializeField] private RectTransform infoItemRect;
	private Vector2 hiddenPos;
	private Vector2 visiblePos;
	
	
	
	[Header("DOMove")]
	[SerializeField] private Transform startPosMenu;
	[SerializeField] private Transform endPosMenu;

	private RectTransform rect;
	private RectTransform startRect;
	private RectTransform endRect;
	private void Awake()
	{
		rect = infoItem.GetComponent<RectTransform>();
		startRect = startPosMenu.GetComponent<RectTransform>();
		endRect = endPosMenu.GetComponent<RectTransform>();
		rect.anchoredPosition = startRect.anchoredPosition;
	}


	private void Start()
	{
		rect = infoItem.GetComponent<RectTransform>();
    
		hiddenPos = new Vector2(0, 0);  
		visiblePos = new Vector2(-400, 0);   

		rect.anchoredPosition = hiddenPos;
	}


	public void AddInvenotryItem(ItemData obj)
	{
		GameObject go = Instantiate(itemPrefab, container);
		itemSlot slot = go.GetComponent<itemSlot>();
		
		slot.UpdateVisuel(obj , this);
	}

	public void OnClick(ItemData obj)
	{
		iconItem.sprite = obj.icon;
		txtItem.text = obj.description;

		rect.DOKill();
		rect.anchoredPosition = startRect.anchoredPosition;
		rect.DOAnchorPos(endRect.anchoredPosition, 0.5f).SetEase(Ease.OutQuint);
	}


	
}
