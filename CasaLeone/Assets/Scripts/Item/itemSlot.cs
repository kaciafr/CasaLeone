using System;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class itemSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        public ItemData currentItem;
        InfoList items;
        private void Start()
        {
            itemImage =  GetComponent<Image>();
            items = InfoList.Instance;
        }
        public void UpdateVisuel(ItemData itemData)
        {
            itemImage.sprite = itemData.icon;
            currentItem = itemData;
        }

        public void OnClick()
        {
            items.UpdateVisual(currentItem);
        }
    
    }
}
