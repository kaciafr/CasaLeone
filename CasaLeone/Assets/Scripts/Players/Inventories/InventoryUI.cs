using System.Collections.Generic;
using Restaurants;
using UnityEngine;

namespace Players.Inventories
{
	public class InventoryUI : MonoBehaviour
	{
		[SerializeField] private Inventory inventory;
		[SerializeField] private GlobalPlayer player;
		[SerializeField] private Transform itemContainer;
		[SerializeField] private GameObject itemPrefab;
		[SerializeField] private List<ItemUI> items;

		private void Start()
		{
			inventory = player.Inventory; 
			
			inventory.OnDishAdded += AddUi;
			inventory.OnDishRemoved += RemoveUi;
		}

		private void OnDisable()
		{
			inventory.OnDishAdded -= AddUi;
			inventory.OnDishRemoved -= RemoveUi;
		}

		public void AddUi(Dish obj)
		{
			GameObject item = Instantiate(itemPrefab, itemContainer);
			ItemUI image = item.GetComponent<ItemUI>();
			image.Init(obj);
		
			items.Add(image);
		}

		public void RemoveUi(Dish obj)
		{
			ItemUI uiToRemove = items.Find(x => x.data.ID == obj.ID);
			Debug.Log(uiToRemove.data.name);
			items.RemoveAt(0);
			Destroy(uiToRemove.gameObject);
		
		}
	}
}

