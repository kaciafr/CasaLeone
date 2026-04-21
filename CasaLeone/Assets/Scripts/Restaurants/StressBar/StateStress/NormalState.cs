using Restaurants;
using UnityEngine;

public class NormalState : IStressBar
{
	private int maxNormal = 25;

	public void Enter(Restaurant restaurant)
	{
		Debug.Log("NormalState");
		restaurant.qteSysteme.round = 1;
	}

	public void Update(Restaurant restaurant, float deltaTime)
	{
		if (restaurant.Stress >= maxNormal)
		{
			LightStressState lightStressState = new LightStressState();
			restaurant.StressGoTo(lightStressState);
		}
	}

	public void Exit(Restaurant restaurant)
	{
	}
}
