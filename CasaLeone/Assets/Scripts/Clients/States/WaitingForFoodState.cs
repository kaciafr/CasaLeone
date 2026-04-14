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

		public WaitingForFoodState(Command command,Dish dishs)
		{
			this.commanded = command;
			this.dish = dishs;
		}

		public void Enter(ClientController controller)
		{
			Debug.Log($"{controller} Veut Graille Violent");
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
				Restaurant.Instance.RemoveCommand(commanded);
				EatingState eatingState = new EatingState(20);
				controller.GoTo(eatingState);
			}
		}
	}
}