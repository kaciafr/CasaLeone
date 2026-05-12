using System;
using PnjWaves;
using Restaurants;
using UnityEngine;

namespace Clients.States
{
	public class WaitingState : IClientState
	{
		private float maxBoredTimed = 1200f;
		
		private float currentBoredTime;
		private bool IsBored => currentBoredTime > maxBoredTimed;
		public void Enter(ClientController controller)
		{
			currentBoredTime = 0;
			maxBoredTimed = controller.maxBoredTime;
			QueueManager.Instance.JoinTheQueue(controller);
			
		}

		public void Exit(ClientController controller)
		{
			QueueManager.Instance.LeaveTheQueue(controller);	
		}

		public void Update(ClientController controller, float deltaTime)
		{
			currentBoredTime += deltaTime;
			
			if (IsBored)
			{
				LeavingState leavingState = new LeavingState(true);
				controller.GoTo(leavingState);
				return;
			}

			if (Restaurant.Instance.TryFindTable(controller.CurrentId, controller.groupSize, out ClientTable table))
			{
				if (table.TryGetSeat(controller.CurrentId,out ClientSeat seat))
				{
					controller.CurrentSeat = seat;
					seat.Reserve(controller);
					GoingToSeatState state = new GoingToSeatState(seat);
					controller.GoTo(state);
				}
			}
		}
	}
}