using Players;
using Players.Interaction;

namespace Clients.States
{
	public interface IInteractableClientState : IClientState
	{
		void Interact(ClientController controller, GlobalPlayer globalPlayer);
	}
}