namespace Clients.States
{
	//TODO tout
	public class EatingState : IClientState
	{
		private readonly float maxEatingTime;
		public float CurrentEatingTime { get; private set; }
		public bool isFinished => CurrentEatingTime >= maxEatingTime;

		public EatingState(float maxEatingTime)
		{
			this.maxEatingTime = maxEatingTime;
		}
		public void Enter(ClientController controller)
		{
			CurrentEatingTime=0;
		}

		public void Exit(ClientController controller)
		{
		}

		public void Update(ClientController controller, float deltaTime)
		{
			CurrentEatingTime += deltaTime;

			if (isFinished)
			{
				CheckingState checkingState = new CheckingState();
				controller.GoTo(checkingState);
			}
		}
	}
}