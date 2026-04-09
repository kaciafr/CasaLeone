namespace Clients.States
{
	public class WaitingState : IClientState
	{
		public void Enter(ClientController controller)
		{
			
		}

		public void Exit(ClientController controller)
		{
			
		}

		public void Update(ClientController controller, float deltaTime)
		{
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