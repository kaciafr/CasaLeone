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
			Debug.Log(Seat.transform.position);
		}

		public void Exit(ClientController controller)
		{
			controller.Movement.ClearDestination();
		}

		public void Update(ClientController controller, float deltaTime)
		{
			if (controller.Movement.HasArrived())
			{
				ReflexionState reflexionState = new ReflexionState(10, 30);
				controller.GoTo(reflexionState);
			}
		}
	}
}