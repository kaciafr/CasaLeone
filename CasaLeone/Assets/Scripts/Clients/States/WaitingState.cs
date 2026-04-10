using System;

namespace Clients.States
{
	public class WaitingState : IClientState
	{
		private readonly float maxBoredTime;
		
		private float currentBoredTime;
		private bool isBored => currentBoredTime > maxBoredTime;
		public void Enter(ClientController controller)
		{
			
			currentBoredTime = 0;
		}

		public void Exit(ClientController controller)
		{
			
		}

		public void Update(ClientController controller, float deltaTime)
		{
			currentBoredTime += deltaTime;
			
			if (isBored)
			{
				LeavingState leavingState = new LeavingState(true);
				controller.GoTo(leavingState);
			}
			
			if (Restaurant.Instance.TryFindTable(out ClientTable table))
			{
				if (table.TryGetSeat(out ClientSeat seat))
				{
					GoingToSeatState state = new GoingToSeatState(seat);
					controller.GoTo(state);
				}
			}
		}
	}
}