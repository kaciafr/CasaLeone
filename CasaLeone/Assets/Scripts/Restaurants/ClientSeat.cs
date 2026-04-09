using UnityEngine;

namespace Clients
{
	public class ClientSeat : MonoBehaviour
	{
		public bool IsFree => Client == null;
		
		[field: SerializeField]
		public ClientController Client { get; private set; }
		
		public void Reserve(ClientController pnj)
		{
			Client = pnj;
		}

		public void Leave()
		{
			Client = null;
		}
	}
}

