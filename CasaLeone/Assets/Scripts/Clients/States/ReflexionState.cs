using System;
using Players;
using Players.Inventories;
using Restaurants;
using UnityEngine;

namespace Clients.States
{
	[Serializable]
	public class ReflexionState : IInteractableClientState
	{
		private readonly float maxReflexionTime;
		private readonly float maxBoredTime;

		public event Action OnReady;
		
		[field: SerializeField]
		public float CurrentReflexionTime { get; private set; }
		[field: SerializeField]
		public Dish Dish { get; private set; }

		public bool IsReady => CurrentReflexionTime >= maxReflexionTime;
		
		public ReflexionState(float maxReflexionTime, float maxBoredTime)
		{
			this.maxReflexionTime = maxReflexionTime;
			this.maxBoredTime = maxBoredTime;
		}
		
		public void Enter(ClientController controller)
		{
			Debug.Log("Entering ReflexionState");
			Dish = Restaurant.Instance.GetRandomDish();
			CurrentReflexionTime = 0;
		}

		public void Exit(ClientController controller)
		{
			OnReady =  null;
		}

		public void Update(ClientController controller, float deltaTime)
		{
			bool wasReady = IsReady;
			CurrentReflexionTime += deltaTime;

			if (!wasReady && IsReady)
			{
				OnReady?.Invoke();
				Debug.Log("ImReady");	
			}
			
			if (IsReady && CurrentReflexionTime >= maxBoredTime)
			{
				LeavingState leavingState = new LeavingState(true);
				controller.GoTo(leavingState);
			}
		}

		public void Interact(ClientController controller, GlobalPlayer globalPlayer)
		{
			Command command =  new Command();
			command.dish = Dish;
			command.client =  controller;
			
			if (IsReady)
			{
				WaitingForFoodState waitingForFoodState = new WaitingForFoodState(command,command.dish);
				Debug.Log($"Interacting with {controller}");
				Restaurant.Instance.AddCommand(command);
				controller.GoTo(waitingForFoodState);
			}
		}

		
	}
}