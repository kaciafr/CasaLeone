using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class menuOfTheRestaurant :  MonoBehaviour
{
	[SerializeField]private string[] plats = { "un Steak", "des Pâtes", " une Salade" };
	
	public Action<menuOfTheRestaurant> menu;
	private pnjMove currentClient;
	
	public void StartTakeOrder(pnjMove pnj)
	{
		currentClient = pnj;
		currentClient.whatTheyWhant.Clear();
		WhatTheyWant();
	}

	private void WhatTheyWant()
	{
		int rand = Random.Range(0, plats.Length);
		Debug.Log($"{currentClient} voudrait {plats[rand]} !");
		currentClient.whatTheyWhant.Add(plats[rand]);
		currentClient.logic = pnjMove.cycle.Timer;
	}
}
