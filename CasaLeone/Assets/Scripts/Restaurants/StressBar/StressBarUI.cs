using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Restaurants.UI
{
    public class StressBarUI : MonoBehaviour
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

            if (Restaurant.Instance == null)
            {
                Debug.LogError("Restaurant.Instance est null dans StressBarUI !");
                return;
            }
            Restaurant.Instance.OnStressChanged += UpdateBar;
        }

        private void OnDisable()
        {
            if (Restaurant.Instance == null) return;
            Restaurant.Instance.OnStressChanged -= UpdateBar;
        }

        private void UpdateBar(float last, float current)
        {
            float t = current / (float)Restaurant.MaxStress;
            anguishBarImage.fillAmount = t;

            damageBarImage.DOKill();
            damageBarImage.DOFillAmount(t, .3f).SetEase(Ease.InCubic);
        }
    }
}