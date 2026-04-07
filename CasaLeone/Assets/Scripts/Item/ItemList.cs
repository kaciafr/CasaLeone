using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ItemList: MonoBehaviour
    {
        [SerializeField] private List<ItemData> items;
        [SerializeField] private GameObject itemSlot;
        [SerializeField] private itemSlot itemSlots;
        [SerializeField] private Transform containt;
    
        public void UpdateList(ItemData item)
        {
            items.Add(item);
            itemSlots.UpdateVisuel(item);
            Instantiate(itemSlot, containt);
        }
    }
}
