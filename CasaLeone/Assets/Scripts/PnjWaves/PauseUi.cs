using DG.Tweening;
using PnjWaves;
using UnityEngine;

public class PauseUi : MonoBehaviour
{
	[SerializeField] private RectTransform pauseUi;
	
	[SerializeField] private WaveSpawner waveSpawner;

	private void OnEnable()
	{
		waveSpawner.Pause += UiActive;
		waveSpawner.EndPause += UiInactive;
	}

	private void OnDisable()
	{
		waveSpawner.Pause -= UiActive;
		waveSpawner.EndPause -= UiInactive;
	}
	

	private void UiActive(WaveSpawner obj)
	{
		PlayAnimation(pauseUi, true);
	}

	private void UiInactive(WaveSpawner obj)
	{
		PlayAnimation(pauseUi, false);
	}
	
	private void PlayAnimation(RectTransform moveUI, bool show)
	{
		RectTransform rect = moveUI.GetComponent<RectTransform>();

		if (show)
		{
			rect.DOKill(false);
			rect.pivot = new Vector2(0.5f, 1f);
			rect.localScale = new Vector3(0.7f, 0.7f, 0.2f);
			rect.DOScaleY(0.7f, 0.8f).SetEase(Ease.OutBounce);
		}
		else
		{
			rect.DOKill(false);
			rect.DOScaleY(0f, 0.4f).SetEase(Ease.InCubic);
		}
	}
}
