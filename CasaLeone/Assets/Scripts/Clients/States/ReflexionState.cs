using System;
using Players;
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
			Dish = Restaurant.Instance.GetRandomDish();
			CurrentReflexionTime = 0;
		}

		public void Exit(ClientController controller)
		{
			
		}

		public void Update(ClientController controller, float deltaTime)
		{
			bool wasReady = IsReady;
			CurrentReflexionTime += deltaTime;
			
			if(!wasReady && IsReady)
				OnReady?.Invoke();
			
			if (IsReady && CurrentReflexionTime >= maxBoredTime)
			{
				LeavingState leavingState = new LeavingState(true);
				controller.GoTo(leavingState);
			}
		}

		public void Interact(ClientController controller, GlobalPlayer globalPlayer)
		{
			if (IsReady)
			{
				WaitingForFoodState waitingForFoodState = new WaitingForFoodState(Dish);
				controller.GoTo(waitingForFoodState);
			}
		}
	}
}