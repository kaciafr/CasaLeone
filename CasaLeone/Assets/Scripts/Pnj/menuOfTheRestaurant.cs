using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class menuOfTheRestaurant :  MonoBehaviour
{
	//[SerializeField]private string[] plats = { "un Steak", "des Pâtes", " une Salade" };
	[SerializeField] private List<Ingrediente> plats;
	[SerializeField] private ListMove list;
	public Action<menuOfTheRestaurant> menu;
	public int rand;
	private pnjMove currentClient;
	
	public void StartTakeOrder(pnjMove pnj)
	{
		currentClient = pnj;
		currentClient.whatTheyWhant = null;
		WhatTheyWant();
	}

	private void WhatTheyWant()
	{
		rand = Random.Range(0, plats.Count);
		Debug.Log($"{currentClient} voudrait {plats[rand]} !");
		currentClient.whatTheyWhant = plats[rand];
		currentClient.logic = pnjMove.cycle.Timer;
		list.UpdateList(rand);
	}
}
