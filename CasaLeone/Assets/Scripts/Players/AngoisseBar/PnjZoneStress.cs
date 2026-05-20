using System;
using Restaurants;
using UnityEngine;

public class PnjZoneStress : MonoBehaviour
{
	public bool addStress;
	[SerializeField] private float stress;

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			if (addStress)
			{
				Restaurant.Instance.AddOrRemoveStress(stress);
			}
			if(!addStress)
			{
				Debug.Log("NANI");
				Restaurant.Instance.AddOrRemoveStress(-stress);
			}
		}
	}
}
