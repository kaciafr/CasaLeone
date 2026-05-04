using Clients;
using Players;
using Players.Interaction;
using PnjWaves;
using UnityEngine;

namespace Restaurants
{
	public class ClientTable : MonoBehaviour, IInteractableQTE
	{
		[field: SerializeField]
		public ClientSeat[] ClientSeats { get; private set; }

		public int currentIdGroupe { get; set; } = -1;

		public bool IsFree
		{
			get
			{
				if(currentIdGroupe != -1) return false;
				for (int i = 0; i < ClientSeats.Length; i++)
				{
					if (ClientSeats[i] is { IsFree: false })
						return false;  
				}
				
				return true;
			}
		}

		public bool TryGetSeat(int idGroup,out ClientSeat seat)
		{
			if (currentIdGroupe == -1 || currentIdGroupe == idGroup)
			{
				for (int i = 0; i < ClientSeats.Length; i++)
				{
					if (ClientSeats[i].IsFree)
					{
						seat = ClientSeats[i];
						currentIdGroupe = idGroup;
						return true;
					}
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

		public bool CanFitGroup(int idGroup,int groupSize)
		{
			if(currentIdGroupe != -1&& currentIdGroupe != idGroup) return false;
			
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

		public void CheckIfTableIsNowEmpty()
		{
			int occupiedCount = 0;
			foreach (var seat in ClientSeats)
			{
				if (!seat.IsFree) occupiedCount++;
			}

			if (occupiedCount == 0)
			{
				currentIdGroupe= -1;
				Debug.Log("La table est maintenant totalement libre.");
			}
		}
	}
}