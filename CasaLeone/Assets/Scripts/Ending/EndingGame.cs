using DG.Tweening;
using Ending;
using PnjWaves;
using UnityEngine;

public class EndingGame : MonoBehaviour
{
	[Header("FireEnd")]
	[SerializeField] private GameObject fireEnd;
	[SerializeField] private WaveSpawner waveSpawner;
	public EndScript endScript;
	private void Start()
	{
		fireEnd.transform.localScale = Vector3.zero;
		endScript.fireEnd = false;
		endScript.burnOutEnd = false;
		endScript.changedEnd = false;
		endScript.perroquetEnd = false;
	}

	private void OnEnable()
	{
		waveSpawner.FireEnd += FireEnd;
	}

	private void OnDisable()
	{
		waveSpawner.FireEnd -= FireEnd;
	}

	private void FireEnd(int wave)
	{
		if (wave >= 2)
		{
			fireEnd.transform.DOKill(true);
			fireEnd.transform.DOScale(0.5f,0.5f).SetEase(Ease.OutBack);
		}
	}
    
}
