using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    public class ItemList: MonoBehaviour
    {
        [SerializeField] private GameObject itemSlotPrefab;
        [SerializeField] private Transform container;
        [SerializeField] private InvRead invRead;
        public void UpdateList(ItemData item)
        {
            GameObject newSlot = Instantiate(itemSlotPrefab, container);
            itemSlot slot = newSlot.GetComponent<itemSlot>();
            slot.UpdateVisuel(item, invRead);

            
            invRead.AddInvenotryItem(item);
        }
    }
}
