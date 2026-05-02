using System;
using System.Collections.Generic;
using Clients;
using Players;
using PnjWaves;
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
		public Transform Exit  { get; private set; }
		
		[field: SerializeField]
		public GlobalPlayer[] Players { get; private set; }


		public ClientTable[] TablePlaces => tablePlaces;
		
		[field: SerializeField]
		public QTESysteme.QTESysteme qteSysteme;
		[field: SerializeField, Range(0, MaxStress)]
		public int Stress { get; private set; } = 0;
		public event Action <IStressBar> OnStressStateChanged;
		public IStressBar currentStressBar { get; private set; }
		public bool TryFindTable(int groupID,int groupSize, out ClientTable table)
		{
			for (var i = 0; i < tablePlaces.Length; i++)
			{
				var t = tablePlaces[i];
				if (t.currentIdGroupe == groupID && t.GetFreeSeatCount() > 0)
				{
					table = t;
					return true;
				}
			}

			for (var i = 0; i < tablePlaces.Length; i++)
			{
				var t = tablePlaces[i];
				if (t.currentIdGroupe==-1 && t.CanFitGroup(groupID,groupSize))
				{
					table = t;
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

		private void Start()
		{
			StressGoTo(new NormalState());
		}

		private void Update()
		{
			currentStressBar?.Update(this,Time.deltaTime);
		}

		public void StressGoTo(IStressBar stressBar)
		{
			currentStressBar?.Exit(this);
			
			currentStressBar = stressBar;
			
			currentStressBar?.Enter(this);
			
			OnStressStateChanged?.Invoke(currentStressBar);
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
