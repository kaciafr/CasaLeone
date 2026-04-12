using System;
using Restaurants;

namespace Clients.States
{
	public class WaitingState : IClientState
	{
		private float maxBoredTimed;
		
		private float currentBoredTime;
		private bool IsBored => currentBoredTime > maxBoredTimed;
		public void Enter(ClientController controller)
		{
			
			currentBoredTime = 0;
			maxBoredTimed = controller.maxBoredTime;
			controller.Movement.SetDestination(Restaurant.Instance.OutSide.transform.position);
		}

		public void Exit(ClientController controller)
		{
			
		}

		public void Update(ClientController controller, float deltaTime)
		{
			currentBoredTime += deltaTime;
			//TODO Regler l'attente
			/*if (IsBored)
			{
				LeavingState leavingState = new LeavingState(true);
				controller.GoTo(leavingState);
			}*/

			if (Restaurant.Instance.TryFindTable(out ClientTable table))
			{
				if (table.TryGetSeat(out ClientSeat seat))
				{
					seat.Reserve(controller);
					GoingToSeatState state = new GoingToSeatState(seat);
					controller.GoTo(state);
				}
			}
			else
			{
				controller.Movement.SetDestination(Restaurant.Instance.OutSide.position);
			}
		}
	}
}