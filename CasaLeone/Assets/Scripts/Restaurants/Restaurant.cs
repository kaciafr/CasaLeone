using System;
using System.Collections.Generic;
using ListForEat;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clients
{
	public class Restaurant : Singleton<Restaurant>
	{
		[SerializeField] 
		private List<Ingrediente> plats;
		public Action<Restaurant> Menu;
		public int rand;

		[SerializeField]
		private ClientTable[] tablePlaces;
		
		[field: SerializeField]
		public Transform OutSide { get; private set; }
		[field: SerializeField]
		public Transform Exit  { get; private set; }
		public bool TryFindTable(out ClientTable table)
		{
			foreach (ClientTable clientTable in tablePlaces)
			{
				if (clientTable.IsFree)
				{
					table = clientTable;
					return true;
				}
			}
			
			table = null;
			return false;
		}
		
		public Ingrediente GetRandomDish()
		{
			rand = Random.Range(0, plats.Count);
			return plats[rand];
		}
	}
}
