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

		public GameObject yellowBird;
		private void Awake()
		{
			Inventory = new Inventory();
		}

		private void Start()
		{
			yellowBird.SetActive(false);
		}
	}
}