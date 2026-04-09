using System;
using System.Collections.Generic;
using ListForEat;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pnj
{
	public class MenuOfTheRestaurant : Singleton<MenuOfTheRestaurant>
	{
		//[SerializeField]private string[] plats = { "un Steak", "des Pâtes", " une Salade" };
		[SerializeField] private List<Ingrediente> plats;
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
		}
	}
}
