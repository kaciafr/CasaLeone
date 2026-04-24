using System;
using Restaurants;
using UnityEngine;

namespace Clients.States
{
	public class LeavingState : IClientState
	{
		public readonly bool IsAngry;
		private bool isReady = true;
		public event Action ReplicaLine;

		public LeavingState(bool isAngry)
		{
			IsAngry = isAngry;
		}

		public void Enter(ClientController controller)
		{
			isReady = true;
			if (controller.currentSeat != null)
			{
				ClientTable table = controller.currentSeat.table;
				controller.currentSeat.Leave(controller); 
				table.CheckIfTableIsNowEmpty(); 
				controller.currentSeat = null; 
			}
			
			controller.Movement.SetDestination(Restaurant.Instance.Exit);
			if (IsAngry)
			{
				Restaurant.Instance.AddOrRemoveStress(5);
			}
			else
			{
				Restaurant.Instance.AddOrRemoveStress(-2);
			}
		}

		public void Exit(ClientController controller)
		{
			if(controller.Movement.HasArrived())
				controller.Despawn();
		}

		public void Update(ClientController controller, float deltaTime)
		{
			if (isReady)
			{
				ReplicaLine?.Invoke();
				isReady = false;
			}
			
			if (controller.Movement.HasArrived())
				controller.GoTo(null);
		}
	}
}