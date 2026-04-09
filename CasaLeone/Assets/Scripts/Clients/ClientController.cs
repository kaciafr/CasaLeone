using System;
using Clients.States;
using Inventories;
using ListForEat;
using UnityEngine;

namespace Clients
{
	public class ClientController : MonoBehaviour
	{
		public event Action<IClientState> OnStateChanged;
		
		[field: SerializeField]
		public ClientMovement Movement { get; private set; }
		
		
		public IClientState CurrentState { get; private set; }

		private void Update()
		{
			CurrentState?.Update(this, Time.deltaTime);
			
		}

		public void GoTo(IClientState state)
		{
			CurrentState?.Exit(this);
			
			CurrentState = state;
			
			CurrentState?.Enter(this);
			
			OnStateChanged?.Invoke(CurrentState);
		}

		public void Despawn()
		{
			Destroy(gameObject);
		}

		public void Interact()
		{
			
			if(CurrentState is IInteractableClientState interactableState)
				interactableState.Interact(this);
			/*
			switch (CurrentState)
			{
				case ReflexionState reflexionState:
					if (reflexionState.IsReady)
					{
						Ingrediente dish = reflexionState.Dish;
						WaitingForFoodState waitingForFoodState = new WaitingForFoodState(dish);
						GoTo(waitingForFoodState);
					}
					break;
				case WaitingForFoodState waitingForFoodState:
					var inventory = Inventory.Instance;
					if (inventory.ingredientes.Contains(waitingForFoodState.dish))
					{
						inventory.ingredientes.Remove(waitingForFoodState.dish);
						EatingState eatingState = new EatingState();
						GoTo(eatingState);
					}
					break;
			}
			*/
		}
	}
}