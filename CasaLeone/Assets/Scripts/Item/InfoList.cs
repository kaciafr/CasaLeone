using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
	public class InfoList : Singleton<InfoList>
	{
		[SerializeField] private GameObject infoPrefab;
		[SerializeField] private Image icon;
		[SerializeField] private TextMeshProUGUI text;
		
		[SerializeField] private GameObject startButton;
		[SerializeField] private GameObject endButton;

		private bool isClicked = true;

		private void Start()
		{ 
			infoPrefab.transform.DOMove(startButton.transform.position, 0.1f);
		}
		public void UpdateVisual(ItemData item)
		{
			isClicked = !isClicked;
			if (!isClicked)
			{
				infoPrefab.SetActive(true);
				
				infoPrefab.transform.DOMove(startButton.transform.position, 0.1f);
				infoPrefab.transform.DOMove(endButton.transform.position, 0.1f);
				icon.sprite = item.icon;
				text.text = item.description;
			}
			else
			{
				infoPrefab.transform.DOMove(startButton.transform.position, 0.1f);
			}
		}

		public void ClosesInfo()
		{
			infoPrefab.SetActive(false);
		}
	}
}