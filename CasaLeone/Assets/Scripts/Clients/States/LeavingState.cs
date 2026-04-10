using System;
using Restaurants;
using UnityEngine;

namespace Clients.States
{
	public class LeavingState : IClientState
	{
		public readonly bool IsAngry;

		public LeavingState(bool isAngry)
		{
			IsAngry = isAngry;
		}

		public void Enter(ClientController controller)
		{
			//TODO Passer par le player
			if (IsAngry)
				Restaurant.Instance.AddOrRemoveStress(5);
			else
				Restaurant.Instance.AddOrRemoveStress(-2);
			
			
			controller.Movement.SetDestination(Restaurant.Instance.Exit);
		}

		public void Exit(ClientController controller)
		{
			controller.Despawn();
		}

		public void Update(ClientController controller, float deltaTime)
		{
			if (controller.Movement.HasArrived())
				controller.GoTo(null);
		}
	}
}