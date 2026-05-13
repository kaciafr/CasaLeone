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
		[field: SerializeField]
		public Inventory Inventory { get; private set; }
		public PlayerInput playerMovement;
		[SerializeField] private WaveSpawner waveSpawner;
		public GameObject yellowBird;
		private void Awake()
		{
			Inventory = new Inventory();
		}

		private void Start()
		{
			yellowBird.SetActive(false);
			
		}

		private void OnEnable()
		{
			waveSpawner.ClearInventory += Clear;
		}

		private void OnDisable()
		{
			waveSpawner.ClearInventory -= Clear;
		}

		private void Clear(WaveSpawner obj)
		{
			Inventory.Clear();
		}
	}
}