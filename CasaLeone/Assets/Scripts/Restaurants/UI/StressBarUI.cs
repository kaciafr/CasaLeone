using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Restaurants.UI
{
	public class StressBarUI: MonoBehaviour
	{
		[SerializeField]
		private Image anguishBarImage;
		
		[FormerlySerializedAs("DammageBarImage")] 
		[SerializeField]
		private Image damageBarImage;

		private void Start()
		{
			anguishBarImage.fillAmount = 0;
			damageBarImage.fillAmount = 0;
		}


		private void OnEnable()
		{
			Restaurant.Instance.OnStressChanged += UpdateBar;
		}
		private void OnDisable()
		{
			Restaurant.Instance.OnStressChanged -= UpdateBar;
		}

		private void UpdateBar(int last, int current)
		{
			float t = current / (float)Restaurant.MaxStress;
			anguishBarImage.fillAmount = t;

			damageBarImage.DOKill();
			damageBarImage.DOFillAmount(t, .3f).SetEase(Ease.InCubic);
		}
	}
}
