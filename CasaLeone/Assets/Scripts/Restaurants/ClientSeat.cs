using Clients;
using PnjWaves;
using UnityEngine;

namespace Restaurants
{
	public class ClientSeat : MonoBehaviour
	{
		[SerializeField] public ClientTable table; 
		public bool IsFree => Client == null;
		
		[field: SerializeField]
		public ClientController Client { get; private set; }

		public bool reserve;
		public int idGroupe;

		private void Start()
		{
			idGroupe = -1;
		}
		public void Reserve(ClientController pnj)
		{
			Client = pnj;
			idGroupe = pnj.ClientData.idGroupe;
			pnj.CurrentSeat = this;
			if (Client != null)
			{
				reserve =  true;
			}
		}

		public void Leave(ClientController pnj)
		{
			idGroupe = -1;
			pnj.CurrentSeat = null;
			if (Client == null)
			{
				reserve = false;
				Debug.Log(reserve);
			}
		}
	}
}

