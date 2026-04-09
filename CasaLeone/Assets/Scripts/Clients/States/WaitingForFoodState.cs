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
			throw new System.NotImplementedException();
		}

		public void Exit(ClientController controller)
		{
			throw new System.NotImplementedException();
		}

		public void Update(ClientController controller, float deltaTime)
		{
			throw new System.NotImplementedException();
		}

		public void Interact(ClientController controller)
		{
			//TODO passer par le player
			var inventory = Inventory.Instance;
			if (inventory.ingredientes.Contains(dish))
			{
				inventory.ingredientes.Remove(dish);
				EatingState eatingState = new EatingState();
				controller.GoTo(eatingState);
			}
		}
	}
}