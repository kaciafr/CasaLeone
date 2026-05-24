using PnjWaves;
using UnityEngine;

namespace Players.Inventories
{
	public class InventoryManager :  Singleton<InventoryManager>
	{
		[field: SerializeField] public GlobalInventory GlobalInventory { get; private set; }
		[SerializeField] private WaveSpawner waveSpawner;
		
		private void Awake()
		{
			GlobalInventory = new GlobalInventory();
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
			GlobalInventory.Clear();
		}
	}
}