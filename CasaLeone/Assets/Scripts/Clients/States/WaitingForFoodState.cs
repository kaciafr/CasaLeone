using System;
using Players;
using Players.Inventories;
using Restaurants;
using UnityEngine;

namespace Clients.States
{
	public class WaitingForFoodState : IInteractableClientState
	{
		public readonly Command commanded;
		public readonly Dish dish;
		private readonly float startTime = 20;
		private readonly float maxBoredTime = 60;
		public float currentTime;
		public bool isBored => currentTime >= startTime;
		public bool wasReady;

		public event Action Bored;

		public WaitingForFoodState(Command command,Dish dishs)
		{
			this.commanded = command;
			this.dish = dishs;
		}

		public void Enter(ClientController controller)
		{
			currentTime = 0;
		}

		public void Exit(ClientController controller)
		{
		}

		public void Update(ClientController controller, float deltaTime)
		{
			wasReady = isBored;
			currentTime += deltaTime;
			
			if (!wasReady && isBored)
			{
				Bored?.Invoke();
				Debug.LogError($"{controller} Veut Graille Violent");
			}
			
			if (isBored && currentTime > maxBoredTime)
			{
				Restaurant.Instance.RemoveCommand(commanded);
				Debug.LogError($"{controller} Nashave");
				LeavingState leavingState = new LeavingState(true);
				controller.GoTo(leavingState);
			}
		}

		public void Interact(ClientController controller, GlobalPlayer globalPlayer)
		{
			Inventory inventory = globalPlayer.Inventory;
			
			if (inventory.Contains(dish))
			{
				inventory.RemoveDish(dish);
				Restaurant.Instance.RemoveCommand(commanded);
				EatingState eatingState = new EatingState(20);
				controller.GoTo(eatingState);
			}
		}
	}
}