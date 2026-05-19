using DialogueSystem.DATA;
using Players;
using Players.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
	public class Item : MonoBehaviour, IInteractable
	{
		public int Priority => 2;
		[SerializeField] private ItemData itemData;
		[SerializeField] private ItemList itemList;
		
		[SerializeField] private GameObject itemPrefab;
		[SerializeField] private Image icon;
		[SerializeField] private TextMeshProUGUI description;
		private bool seeOneTime = false;
		
		public string conditionID = "";


		private void Start()
		{
			itemPrefab.SetActive(false);
		}
		public void Interact(GlobalPlayer globalPlayer)
		{
			if (!seeOneTime)
			{
				Time.timeScale = 0;
				icon.sprite = itemData.icon;
				description.text = itemData.description;
				itemPrefab.SetActive(true);
				itemList.UpdateList(itemData);
				gameObject.SetActive(false);
				seeOneTime = true;
				ConditionManager.SetCondition(conditionID, true);
			}
		}

	}
}
