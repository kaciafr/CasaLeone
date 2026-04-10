using Inventories;
using ListForEat;

namespace Clients.States
{
	public class WaitingForFoodState : IInteractableClientState
	{
		public readonly Ingrediente dish;

		public WaitingForFoodState(Ingrediente dish)
		{
			this.dish = dish;
		}

		public void Enter(ClientController controller)
		{
		}

		public void Exit(ClientController controller)
		{
		}

		public void Update(ClientController controller, float deltaTime)
		{
		}

		public void Interact(ClientController controller)
		{
			//TODO passer par le player
			var inventory = Inventory.Instance;
			if (inventory.dish.Contains(dish))
			{
				inventory.dish.Remove(dish);
				EatingState eatingState = new EatingState(maxEatingTime:20);
				controller.GoTo(eatingState);
			}
		}
	}
}