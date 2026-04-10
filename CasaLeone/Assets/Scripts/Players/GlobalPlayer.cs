using Inventories;
using ListForEat;
using UnityEngine;

namespace Players
{
	public class GlobalPlayer : MonoBehaviour
	{
		[field: SerializeField]
		public Inventory Inventory { get; private set; }
		[field: SerializeField]
		public AngoisseBar.AngoisseBar AngoisseBar { get; private set; }

		private void Start()
		{
			Inventory = new Inventory();
		}

		public void AddDishClent(Ingrediente dish)
		{
			Inventory.AddDish(dish);
		}
		public void RemoveDishClient(Ingrediente dish)
		{
			Inventory.RemoveDish(dish);
		}

		public void AddAnguidh(float add)
		{
			AngoisseBar.AddAnguish(add);
		}
		public void RemoveAnguidh(float remove)
		{
			AngoisseBar.RemoveAnguish(remove);
		}
		
		
	}
}