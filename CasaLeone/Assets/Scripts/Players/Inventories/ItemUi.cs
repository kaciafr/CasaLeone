using Restaurants;
using UnityEngine;
using UnityEngine.UI;

namespace Players.Inventories
{
    public class ItemUI : MonoBehaviour
    {
        public Dish data;

        public void Init(Dish obj)
        {
            data = obj;
            GetComponent<Image>().sprite = obj.icon;
        }
    }
}
