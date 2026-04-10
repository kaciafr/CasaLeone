using System;
using Players;

namespace Clients.States
{
	public class CheckingState : IInteractableClientState
	{
		public void Enter(ClientController controller)
		{
		}

		public void Exit(ClientController controller)
		{
		}

		public void Update(ClientController controller, float deltaTime)
		{
			
		}

		public void Interact(ClientController controller, GlobalPlayer globalPlayer)
		{
			LeavingState leavingState = new LeavingState(false);
			controller.GoTo(leavingState);
		}
	}
}