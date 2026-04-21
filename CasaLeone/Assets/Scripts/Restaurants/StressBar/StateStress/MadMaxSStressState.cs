using Restaurants;
using UnityEngine;

public class MadMaxSStressState : IStressBar
{
	private float minMadMax = 50;
	public void Enter(Restaurant restaurant)
	{
		
		Debug.Log("MadMAx Enter");
		restaurant.qteSysteme.round = 6;
	}

	public void Update(Restaurant restaurant, float deltaTime)
	{
		if (restaurant.Stress <= minMadMax)
		{
			HightStressState hightStressState = new HightStressState();
			restaurant.StressGoTo(hightStressState);
		}
	}

	public void Exit(Restaurant restaurant)
	{
	}
}
