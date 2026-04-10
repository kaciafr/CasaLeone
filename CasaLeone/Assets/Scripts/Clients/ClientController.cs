using System;
using Clients.States;
using Players;
using PnjWaves;
using Restaurants;
using UnityEngine;

namespace Clients
{
	public class ClientController : MonoBehaviour
	{
		public event Action<IClientState> OnStateChanged;
		
		[field: SerializeField]
		public ClientMovement Movement { get; private set; }
		
		[field: SerializeField]
		public ClientTypeSO ClientData { get; private set; }

		public IClientState CurrentState { get; private set; }
		public WaveProfile WaveProfile { get; private set; }
		
		
		private void Start()
		{
			CurrentState = new WaitingState();
		}

		private void Update()
		{
			CurrentState?.Update(this, Time.deltaTime);
		}
		
		public void GoTo(IClientState state)
		{
			CurrentState?.Exit(this);
			
			CurrentState = state;
			
			CurrentState?.Enter(this);
			
			OnStateChanged?.Invoke(CurrentState);
		}

		public void Spawn(WaveProfile waveProfile)
		{
			WaveProfile = waveProfile;
		}
		
		public void Despawn()
		{
			Destroy(gameObject);
		}

		public void Interact(GlobalPlayer globalPlayer)
		{
			
			if(CurrentState is IInteractableClientState interactableState)
				interactableState.Interact(this, globalPlayer);
		}

		public ClientSeat GetClientSeat()
		{
			ClientTable[] tables = Restaurant.Instance.TablePlaces;
			for (int i = 0; i < tables.Length; i++)
			{
				ClientSeat[] seats = tables[i].ClientSeats;
				for (int j = 0; j < seats.Length; j++)
				{
					if (seats[j].Client == this)
						return seats[j];
				}
			}

			return null;
		}
	}
}