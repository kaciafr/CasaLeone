using System;
using Clients.UI;
using Unity.VisualScripting;

namespace Clients.States
{
	public class CheckingState : IInteractableClientState
	{
		public event Action GetCheckUI;
		public void Enter(ClientController controller)
		{
			GetCheckUI?.Invoke();
		}

		public void Exit(ClientController controller)
		{
		}

		public void Update(ClientController controller, float deltaTime)
		{
			
		}

		public void Interact(ClientController controller)
		{
			LeavingState leavingState = new LeavingState(false);
			controller.GoTo(leavingState);
		}
	}
}