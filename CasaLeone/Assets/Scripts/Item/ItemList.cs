using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    public class ItemList: MonoBehaviour
    {
        [SerializeField] private List<ItemData> items = new List<ItemData>();
        [SerializeField] private GameObject itemSlotPrefab;
        [SerializeField] private Transform container;
    
        public void UpdateList(ItemData item)
        {
	        items.Add(item);
            GameObject newSlot = Instantiate(itemSlotPrefab, container);
            itemSlot slot = newSlot.GetComponent<itemSlot>();
            slot.UpdateVisuel(item);
                    
        }
    }
}
