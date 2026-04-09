using System;
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
			if(IsAngry)
				AngoisseBar.AngoisseBar.Instance.AddAnguish(5f);
			
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