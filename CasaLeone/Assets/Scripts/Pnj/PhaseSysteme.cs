using UnityEngine;

public class PhaseSysteme : MonoBehaviour
{
	public enum cycle
	{
		Waiting,
		Command,
		Order,
		Check,
		Exit
	}

	public cycle logic;
	[SerializeField] private pnjMove target;
	[SerializeField] private Transform exit;
	[SerializeField] private AllPlace allPlace;
	

	private void Start()
	{
	}
	private void Update()
	{
		switch (logic)
		{
			case cycle.Waiting:
				Waiting();
				break;
			case cycle.Command:
				Command();
				break;
			case cycle.Order:
				Order();
				break;
			case cycle.Check:
				EatAndLeave();
				break;
			case cycle.Exit:
				Exit();
				break;
		}
	}
	public void Waiting()
	{
		Debug.Log("Waiting");
	}
	
	public void Command()
	{
		Debug.Log("Command");
	}

	public void Order()
	{
		Debug.Log("Order");
	}
	public void EatAndLeave()
	{
		
	}
	private void Exit()
	{
		
	}
}
