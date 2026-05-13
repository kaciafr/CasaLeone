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
			inventory.OnDishClear += Clear;
		}


		private void OnDisable()
		{
			inventory.OnDishAdded -= AddUi;
			inventory.OnDishRemoved -= RemoveUi;
			inventory.OnDishClear -= Clear;
		}
		private void Clear(Dish obj)
		{
			foreach (ItemUI item in items)
			{
				if (item != null)
				{
					Destroy(item.gameObject);
				}
			}
			items.Clear();
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
			if (uiToRemove != null)
			{
				items.Remove(uiToRemove);
				Destroy(uiToRemove.gameObject);
			}
		}
	}
}

