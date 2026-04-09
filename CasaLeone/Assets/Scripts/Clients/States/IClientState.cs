namespace Clients.States
{
	public interface IClientState
	{
		public void Enter(ClientController controller);
		public void Exit(ClientController controller);
		public void Update(ClientController controller, float deltaTime);
	}
}