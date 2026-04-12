using Players;
using Players.Inventories;
using Restaurants;
using UnityEngine;

namespace Clients.States
{
	public class WaitingForFoodState : IInteractableClientState
	{
		public readonly Dish dish;

		public WaitingForFoodState(Dish dish)
		{
			this.dish = dish;
		}

		public void Enter(ClientController controller)
		{
			Debug.Log("Entering waiting for food");
		}

		public void Exit(ClientController controller)
		{
		}

		public void Update(ClientController controller, float deltaTime)
		{
		}

		public void Interact(ClientController controller, GlobalPlayer globalPlayer)
		{
			Inventory inventory = globalPlayer.Inventory;
			
			if (inventory.Contains(dish))
			{
				inventory.RemoveDish(dish);
				EatingState eatingState = new EatingState(maxEatingTime:20);
				controller.GoTo(eatingState);
			}
		}
	}
}