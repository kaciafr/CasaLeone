using UnityEngine;
using UnityEngine.UI;

public class AngoisseBar : MonoBehaviour
{
	[SerializeField] private GameObject anguishBar;
	[SerializeField] private Image anguishBarImage;

	private void Start()
	{
		anguishBarImage.fillAmount = 0;
	}

	public void AddAnguish(float add)
	{
		anguishBarImage.fillAmount += add;
	}

	public void RemoveAnguish(float remove)
	{
		anguishBarImage.fillAmount -= remove;
	}

}
