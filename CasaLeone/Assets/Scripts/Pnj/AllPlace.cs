using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllPlace : MonoBehaviour
{
	public List<GameObject> tablePlaces;
	public GameObject outSide;
	public Transform FindPlace(GameObject pnj)
	{
		foreach (var canGo in tablePlaces)
		{
			var place = canGo.GetComponent<pnjIN>();

			if (place.canGoIn)
			{
				place.Reserve(pnj);
				return canGo.transform;
			}
		}
		return outSide.transform;
	}
	
}
