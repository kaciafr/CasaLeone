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
			controller.Movement.SetDestination(Restaurant.Instance.Exit);
			if (IsAngry)
				Restaurant.Instance.AddOrRemoveStress(5);
			else
				Restaurant.Instance.AddOrRemoveStress(-2);
		}

		public void Exit(ClientController controller)
		{
			if(controller.Movement.HasArrived())
				controller.Despawn();
		}

		public void Update(ClientController controller, float deltaTime)
		{
			if (controller.Movement.HasArrived())
				controller.GoTo(null);
		}
	}
}