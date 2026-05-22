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
		public PlayersInventory PlayersInventory { get; private set; }
		public PlayerInput playerMovement;
		[SerializeField] private WaveSpawner waveSpawner;
		public GameObject yellowBird;
		private void Awake()
		{
			PlayersInventory = new PlayersInventory();
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
			PlayersInventory.Clear();
		}
	}
}