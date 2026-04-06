using System;
using System.Collections.Generic;
using Pnj;
using UnityEngine;
using Random = UnityEngine.Random;

public class MenuOfTheRestaurant : Singleton<MenuOfTheRestaurant>
{
	//[SerializeField]private string[] plats = { "un Steak", "des Pâtes", " une Salade" };
	[SerializeField] private List<Ingrediente> plats;
	[SerializeField] private ListMove list;
	public Action<MenuOfTheRestaurant> Menu;
	public int rand;
	private PnjMove currentClient;
	public void StartTakeOrder(PnjMove pnj)
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
		currentClient.logic = PnjMove.Cycle.Timer;
		list.UpdateList(rand);
	}
}
