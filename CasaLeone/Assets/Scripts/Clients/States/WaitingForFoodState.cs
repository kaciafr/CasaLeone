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
		private readonly float startTime = 160;
		private readonly float maxBoredTime = 200;
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
			currentTime = 0;
		}

		public void Update(ClientController controller, float deltaTime)
		{
			wasReady = isBored;
			currentTime += deltaTime;
			
			if (!wasReady && isBored)
			{
				Bored?.Invoke();
			}
			
			if (isBored && currentTime > maxBoredTime)
			{
				Restaurant.Instance.RemoveCommand(commanded);
				LeavingState leavingState = new LeavingState(true);
				controller.GoTo(leavingState);
			}
		}

		public void Interact(ClientController controller, GlobalPlayer globalPlayer)
		{
			var globalInventory = globalPlayer.inventoryManager.GlobalInventory;
			
			if (globalInventory.Contains(dish))
			{
				globalInventory.RemoveDish(dish);
				Restaurant.Instance.RemoveCommand(commanded);
				EatingState eatingState = new EatingState(15);
				controller.GoTo(eatingState);
			}
		}
	}
}