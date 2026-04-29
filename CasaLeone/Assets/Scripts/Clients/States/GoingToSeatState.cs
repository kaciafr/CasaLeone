using Restaurants;
using UnityEngine;

namespace Clients.States
{
	public class GoingToSeatState : IClientState
	{
		public readonly ClientSeat Seat;

		public GoingToSeatState(ClientSeat seat)
		{
			this.Seat = seat;
		}

		public void Enter(ClientController controller)
		{
			controller.Movement.SetDestination(Seat.transform);
		}

		public void Exit(ClientController controller)
		{
			controller.Movement.ClearDestination();
		}

		public void Update(ClientController controller, float deltaTime)
		{
			if (controller.Movement.HasArrived())
			{
				ReflexionState reflexionState = new ReflexionState(30, 60);
				controller.GoTo(reflexionState);
			}
		}
	}
}