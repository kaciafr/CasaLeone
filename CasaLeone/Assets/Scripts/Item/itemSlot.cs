using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Item
{
    public class itemSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        private ItemData data;
        private InvRead invRead;

        public void UpdateVisuel(ItemData obj, InvRead inv)
        {
            data = obj;
            invRead = inv;
            itemImage.sprite = obj.icon;

            GetComponent<Button>().onClick.AddListener(() => invRead.OnClick(data));
        }
    }

}
