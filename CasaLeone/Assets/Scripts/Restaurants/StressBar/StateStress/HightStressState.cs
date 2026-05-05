using Restaurants;
using Sound;
using UnityEngine;

public class HightStressState : IStressBar
{
	private int maxHightMedium = 75;
	private int minHightMedium = 40;
	public void Enter(Restaurant restaurant)
	{
		
		Debug.Log("HightStressState Enter");
		if (restaurant.cooker != null)
		{
			restaurant.cooker.round = 4;

		}
		SoundManager.Instance.PlayMusic(MusicType.HighStress);
		AnxietyEffect.instance.SetHightStress();
		
	}

	public void Update(Restaurant restaurant, float deltaTime)
	{
		if (restaurant.Stress >= maxHightMedium)
		{
			MadMaxSStressState madMaxSStressState = new MadMaxSStressState();
			restaurant.StressGoTo(madMaxSStressState);
		}

		if (restaurant.Stress <= minHightMedium)
		{
			LightStressState lightStressState = new LightStressState();
			restaurant.StressGoTo(lightStressState);
		}
	}

	public void Exit(Restaurant restaurant)
	{
	}
}
