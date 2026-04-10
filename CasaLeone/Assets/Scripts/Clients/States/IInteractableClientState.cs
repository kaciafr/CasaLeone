using Players;

namespace Clients.States
{
	public interface IInteractableClientState : IClientState
	{
		void Interact(ClientController controller, GlobalPlayer globalPlayer);
	}
}