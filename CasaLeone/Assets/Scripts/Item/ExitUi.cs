using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
	public class ExitUi : MonoBehaviour
	{
		[SerializeField] private GameObject itemPrefab;
		[SerializeField] private Image icon;
		[SerializeField] private TextMeshProUGUI description;

		public void Interact()
		{
			itemPrefab.SetActive(false);
		}
    
	}
}
