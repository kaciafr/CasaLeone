using System;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class itemSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        private ItemData currentItem;
        
        private void Start()
        {
            itemImage =  GetComponent<Image>();
            
        }
        public void UpdateVisuel(ItemData itemData)
        {
            itemImage.sprite = itemData.icon;
            currentItem = itemData;
        }
    }
}
