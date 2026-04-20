using DG.Tweening;
using Item;
using TMPro;
using UnityEditor.Timeline.Actions;
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
	
	[Header("DOMove")]
	[SerializeField] private GameObject startPosMenu;
	[SerializeField] private GameObject endPosMenu;
	private ItemData currentRead;


	private void Start()
	{
		infoItem.transform.position = startPosMenu.transform.position;
	}

	public void AddInvenotryItem(ItemData obj)
	{
		Debug.LogError("AddInvenotryItem");
		GameObject go = Instantiate(itemPrefab, container);
		itemSlot slot = go.GetComponent<itemSlot>();
		
		slot.UpdateVisuel(obj , this);

		currentRead = obj;
	}
	public void OnClick(ItemData obj)
	{
		iconItem.sprite = obj.icon;    
		txtItem.text = obj.description;
		infoItem.transform.DOMove(endPosMenu.transform.position, 0.5f);
	}
}
