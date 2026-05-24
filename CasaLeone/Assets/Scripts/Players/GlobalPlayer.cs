using Players.Inventories;
using PnjWaves;
using Restaurants;
using Restaurants.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Players
{
	public class GlobalPlayer : MonoBehaviour
	{
		
		[field: SerializeField] public InventoryManager inventoryManager{ get; private set; }
		public PlayerInput playerMovement;
		public GameObject yellowBird;

		private void Start()
		{
			yellowBird.SetActive(false);
			
		}
	}
}