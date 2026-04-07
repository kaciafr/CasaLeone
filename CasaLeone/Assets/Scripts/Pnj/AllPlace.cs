using System.Collections.Generic;
using UnityEngine;

namespace Pnj
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
				var place = canGo.GetComponent<PnjIn>();
				if (place.canGoIn)
				{
					place.Reserve(pnj);
					return canGo.transform;
				}
			}
			return outSide.transform;
		}
	
	}
}
