using System.Collections.Generic;
using ListForEat;
using UnityEngine;

namespace Inventories
{
	public class InventoryUI : MonoBehaviour
	{
		[SerializeField] private Inventory inventory;
		[SerializeField] private Transform itemContainer;
		[SerializeField] private GameObject itemPrefab;
		[SerializeField] private List<ItemUI> items;

		private void OnEnable()
		{
			inventory.TheOrder += AddUi;
			inventory.GiveOrder += RemoveUi;
		}

		private void OnDisable()
		{
			inventory.TheOrder -= AddUi;
			inventory.GiveOrder -= RemoveUi;
		}

		private void AddUi(Ingrediente obj)
		{
			GameObject item = Instantiate(itemPrefab, itemContainer);
			ItemUI image = item.GetComponent<ItemUI>();
			image.Init(obj);
		
			items.Add(image);
		}

		private void RemoveUi(Ingrediente obj)
		{
			ItemUI uiToRemove = items.Find(x => x.data.ID == obj.ID);
			Debug.Log(uiToRemove.data.name);
			items.RemoveAt(0);
			Destroy(uiToRemove.gameObject);
		
		}
	}
}

