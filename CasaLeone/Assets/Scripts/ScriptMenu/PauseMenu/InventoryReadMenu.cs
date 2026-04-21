using DG.Tweening;
using UnityEngine;

public class InventoryReadMenu : MonoBehaviour
{
	[SerializeField] private GameObject readMenu;

	private void Start()
	{
		readMenu.SetActive(false);
	}

	public void ButtonClick()
	{
		readMenu.SetActive(true);
		
	}

	public void Other()
	{
		readMenu.SetActive(false);
		
	}



}
