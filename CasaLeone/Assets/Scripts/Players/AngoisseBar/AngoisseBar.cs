using UnityEngine;
using UnityEngine.UI;

namespace AngoisseBar
{
	public class AngoisseBar: MonoBehaviour
	{
		[SerializeField] private GameObject anguishBar;
		[SerializeField] private Image anguishBarImage;
		[SerializeField] private Image DammageBarImage;

		private void Start()
		{
			anguishBarImage.fillAmount = 0;
			DammageBarImage.fillAmount = 0;
		}

		private void Update()
		{
			if (anguishBarImage.fillAmount <= DammageBarImage.fillAmount)
			{
				DammageBarImage.fillAmount = Mathf.Lerp(DammageBarImage.fillAmount,anguishBarImage.fillAmount, 0.05f);
			}
		}

		public void AddAnguish(float add)
		{
			DammageBarImage.fillAmount += add;
			anguishBarImage.fillAmount += add;
		
		}

		public void RemoveAnguish(float remove)
		{
			anguishBarImage.fillAmount -= remove;
		}

	}
}
