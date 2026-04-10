using Players;
using Players.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
	public class Item : MonoBehaviour, IInteractable
	{
		[SerializeField] private ItemData itemData;
		[SerializeField] private ItemList itemList;
		
		[SerializeField] private GameObject itemPrefab;
		[SerializeField] private Image icon;
		[SerializeField] private TextMeshProUGUI description;

		private void Start()
		{
			itemPrefab.SetActive(false);
		}
		public void Interact(GlobalPlayer globalPlayer)
		{
			itemPrefab.SetActive(true);
			itemList.UpdateList(itemData);
			icon.sprite = itemData.icon;
			description.text = itemData.description;
			Destroy(gameObject);
		}
		public void EndInteraction()
		{
		}
	}
}
