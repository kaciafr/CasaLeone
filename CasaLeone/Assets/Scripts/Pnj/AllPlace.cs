using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllPlace : MonoBehaviour
{
	public List<GameObject> tablePlaces = new List<GameObject>();
	public GameObject outSide;
	public GameObject exit;
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
