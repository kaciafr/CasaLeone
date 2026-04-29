using Restaurants;
using Sound;
using UnityEngine;

public class LightStressState : IStressBar
{
	private int maxLightMedium = 50;
	private int minLightMedium = 20;
	public void Enter(Restaurant restaurant)
	{
		
		Debug.Log("LightStressState Enter");
		restaurant.qteSysteme.round = 2;
		SoundManager.Instance.PlayMusic(MusicType.HighStress);

	}

	public void Update(Restaurant restaurant, float deltaTime)
	{

		if (restaurant.Stress >= maxLightMedium)
		{
			HightStressState  hightStressState = new HightStressState();
			restaurant.StressGoTo(hightStressState);
		}
		if (restaurant.Stress <= minLightMedium)
		{
			NormalState  normalState = new NormalState();
			restaurant.StressGoTo(normalState);
		}
	}

	public void Exit(Restaurant restaurant)
	{
	}
}
