using System;
using System.Collections;
using Clients.States;
using Players;
using Players.Interaction;
using PnjWaves;
using Restaurants;
using UnityEngine;

namespace Clients
{
	public class ClientController : MonoBehaviour,IInteractableQTE
	{
		public event Action<IClientState> OnStateChanged;
		[SerializeField]
		public float maxBoredTime = 5f;

		public int groupSize;

		[field: SerializeField]
		public ClientMovement Movement { get; private set; }
		
		[field: SerializeField]
		public ClientTypeSO ClientData { get; private set; }

		public IClientState CurrentState { get; private set; }
		public WaveProfile WaveProfile { get; private set; }

		[field : SerializeField] 
		public int currentId { get; private set; }
		
		[field : SerializeField] 
		public ClientSeat currentSeat { get; set; }

		private void Start()
		{
			GoTo(new WaitingState());
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

		public void Spawn(WaveProfile waveProfile, int clientsInGroup, int size)
		{
			currentId = ClientData.idGroupe;
			WaveProfile = waveProfile;
			groupSize = size;
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
			return currentSeat;
			
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