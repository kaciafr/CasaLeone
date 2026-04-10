using Players.Inventories;
using Restaurants;
using Restaurants.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Players
{
	public class GlobalPlayer : MonoBehaviour
	{
		[field: SerializeField]
		public Inventory Inventory { get; private set; }

		private void Start()
		{
			Inventory = new Inventory();
		}

		public void AddDishClent(Dish dish)
		{
			Inventory.AddDish(dish);
		}
		public void RemoveDishClient(Dish dish)
		{
			Inventory.RemoveDish(dish);
		}

		
	}
}