using Restaurants;
using Sound;
using UnityEngine;

public class NormalState : IStressBar
{
	private int maxNormal = 25;

	public void Enter(Restaurant restaurant)
	{
		restaurant.cooker.round = 1;
		SoundManager.Instance.PlayMusic(MusicType.NormalStress);
		AnxietyEffect.instance.SetNormal();
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
