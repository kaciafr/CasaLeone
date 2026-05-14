using PnjWaves;
using UnityEngine;

public class RoundEcran : Singleton<RoundEcran>
{
	[field: SerializeField] public int happyScore {get; private set;}
	[field: SerializeField] public int sadScore {get; private set;}
	[field: SerializeField] public int score {get; private set;}
	
	[Header("References")]
	[SerializeField] private WaveSpawner waveSpawner;

	private void Start()
	{
		happyScore =0;
		sadScore =0;
		score = 0;
	}

	private void OnEnable()
	{
		waveSpawner.CleanScore += CleanScore;
	}

	private void OnDisable()
	{
		waveSpawner.CleanScore -= CleanScore;
	}

	private void CleanScore(WaveSpawner obj)
	{
		happyScore = 0;
		sadScore = 0;
	}

	public void HappyScore()
	{
		happyScore++;
		Score();
	}

	public void SadScore()
	{
		sadScore++;
	}

	public void Score()
	{
		score += happyScore;
	}
}
