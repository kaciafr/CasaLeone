using Clients;
using Players;
using Players.Interaction;
using PnjWaves;
using UnityEngine;

namespace Restaurants
{
	public class ClientTable : MonoBehaviour, IInteractable
	{
		[field: SerializeField]
		public ClientSeat[] ClientSeats { get; private set; }

		public bool IsFree
		{
			get
			{
				for (int i = 0; i < ClientSeats.Length; i++)
				{
					if (ClientSeats[i] is { IsFree: false })
						return false;  
				}
				
				return true;
			}
		}

		public bool TryGetSeat(out ClientSeat seat)
		{
			for (int i = 0; i < ClientSeats.Length; i++)
			{
				if (ClientSeats[i].IsFree )
				{
					seat = ClientSeats[i];
					return true;
				}
			}
			
			seat = null;
			return false;
		}

		public void Interact(GlobalPlayer globalPlayer)
		{
			for (int i = 0; i < ClientSeats.Length; i++)
			{
				var seat = ClientSeats[i];
				if (seat.Client)
					seat.Client.Interact(globalPlayer);
			}
		}

		public bool CanFitGroup(int groupSize)
		{
			return GetFreeSeatCount() >= groupSize;
		}
		public int GetFreeSeatCount()
		{
			int count = 0;

			for (int i = 0; i < ClientSeats.Length; i++)
			{
				if (ClientSeats[i].IsFree)
					count++;
			}

			return count;
		}
	}
}