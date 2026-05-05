using Restaurants;
using Sound;
using UnityEngine;

public class MadMaxSStressState : IStressBar
{
	private float minMadMax = 50;
	public void Enter(Restaurant restaurant)
	{
		
		Debug.Log("MadMAx Enter");
		restaurant.cooker.round = 6;
		SoundManager.Instance.PlayMusic(MusicType.MadMaxStress);
		AnxietyEffect.instance.SetHightStress();
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
