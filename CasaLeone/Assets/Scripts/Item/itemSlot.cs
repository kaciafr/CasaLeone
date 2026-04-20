using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Item
{
    public class itemSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;

        public ItemData data;
        private InvRead invRead;
        
        private void Start()
        {
            itemImage =  GetComponent<Image>();

        }
        public void UpdateVisuel(ItemData itemData , InvRead read)
        {
            
            data = itemData;
            invRead = read;

            itemImage.sprite = itemData.icon;
            GetComponent<Button>().onClick.AddListener(() => read.OnClick(data));
            
        }
        
        
    }
}
