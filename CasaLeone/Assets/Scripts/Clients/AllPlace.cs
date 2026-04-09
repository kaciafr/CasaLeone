using System.Collections.Generic;
using UnityEngine;

namespace Clients
{
	public class AllPlace : Singleton<AllPlace>
	{
		public List<GameObject> tablePlaces = new List<GameObject>();
		public GameObject outSide;
		public GameObject exit;
	
		public Transform FindPlace(GameObject pnj)
		{
			foreach (var canGo in tablePlaces)
			{
				var place = canGo.GetComponent<ClientSeat>();
				if (place.IsFree)
				{
					place.Reserve(pnj);
					return canGo.transform;
				}
			}
			return outSide.transform;
		}
	
	}
}
