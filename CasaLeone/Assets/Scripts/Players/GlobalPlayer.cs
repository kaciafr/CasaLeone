using Players.Inventories;
using Restaurants;
using Restaurants.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Players
{
	public class GlobalPlayer : MonoBehaviour
	{
		[field: SerializeField]
		public Inventory Inventory { get; private set; }
		
		public PlayerInput playerMovement;

		private void Awake()
		{
			Inventory = new Inventory();
		}
		
	}
}