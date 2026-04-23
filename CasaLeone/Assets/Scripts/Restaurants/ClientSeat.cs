using Clients;
using UnityEngine;

namespace Restaurants
{
	public class ClientSeat : MonoBehaviour
	{
		[field: SerializeField]
		public bool IsFree => Client == null;
		
		[field: SerializeField]
		public ClientController Client { get; private set; }

		public bool reserve;
		
		public void Reserve(ClientController pnj)
		{
			Client = pnj;
			if (Client != null)
			{
				reserve =  true;
				Debug.Log(reserve);
			}
		}

		public void Leave()
		{
			Client = null;
			if (Client == null)
			{
				reserve = false;
				Debug.Log(reserve);
			}
		}
	}
}

