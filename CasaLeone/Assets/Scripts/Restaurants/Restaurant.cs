using System;
using System.Collections.Generic;
using Clients;
using Players;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Restaurants
{
	public class Restaurant : Singleton<Restaurant>
	{
		public const int MaxStress = 100;
		public event Action<int, int> OnStressChanged;
		public event Action<Command> OnCommandAdded; 
		public event Action<Command> OnCommandRemoved; 
		
		[SerializeField] 
		private List<Dish> plats;

		[SerializeField] 
		private List<Command> commands;
		
		[SerializeField]
		private ClientTable[] tablePlaces;
		
		[field: SerializeField]
		public Transform OutSide { get; private set; }
		[field: SerializeField]
		public Transform Exit  { get; private set; }
		
		[field: SerializeField]
		public GlobalPlayer[] Players { get; private set; }

		[field: SerializeField, Range(0, MaxStress)]
		public int Stress { get; private set; } = 0;

		public ClientTable[] TablePlaces => tablePlaces;
		
		public bool TryFindTable(out ClientTable table)
		{
			for (var i = 0; i < tablePlaces.Length; i++)
			{
				var clientTable = tablePlaces[i];
				if (clientTable.IsFree)
				{
					table = clientTable;
					return true;
				}
			}

			table = null;
			return false;
		}
		
		public Dish GetRandomDish()
		{
			int rand = Random.Range(0, plats.Count);
			return plats[rand];
		}


		public void AddOrRemoveStress(int amount)
		{
			int last = Stress;
			Stress += amount;

			if (Stress < 0)
				Stress = 0;
			if (Stress >= MaxStress)
			{
				Stress = MaxStress;
				GameOver();
			}
			
			OnStressChanged?.Invoke(last, Stress);
		}

		public void AddCommand(Command command)
		{
			commands.Add(command);
			OnCommandAdded?.Invoke(command);
		}

		public void RemoveCommand(Command command)
		{
			commands.Remove(command);
			OnCommandRemoved?.Invoke(command);
		}

		public GlobalPlayer GetPlayer(int playerId) => Players[playerId];
		
		public IEnumerable<Command> GetCommandsForClient(ClientController client)
		{
			foreach (var command in commands)
			{
				if(command.client == client)
					yield return command;
			}
		}
		
		private void GameOver()
		{
			Debug.Log("FF");
		}
	}
}
