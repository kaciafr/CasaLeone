using System;
using Players;

namespace Clients.States
{
	public class CheckingState : IInteractableClientState
	{
		private float maxBoredTimed = 20;
		
		private float currentBoredTime;
		private bool IsBored => currentBoredTime > maxBoredTimed;
		
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
			
			if (IsBored)
			{
				LeavingState leavingState = new LeavingState(false);
				controller.GoTo(leavingState);
				return;
			}
			
		}

		public void Interact(ClientController controller, GlobalPlayer globalPlayer)
		{
			LeavingState leavingState = new LeavingState(false);
			controller.GoTo(leavingState);
		}
	}
}