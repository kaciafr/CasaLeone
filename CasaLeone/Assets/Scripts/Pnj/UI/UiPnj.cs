using DG.Tweening;
using Pnj;
using UnityEngine;

public class UiPnj : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private PnjMove pnj;

	[Header("UI Elements")] 
	[SerializeField] private GameObject whantOrder;
	[SerializeField] private GameObject whaitOrder;
	[SerializeField] private GameObject checkUI;
	private bool isActived = true;

	private void Start()
	{
		pnj.ClientWhantTakeOrder += OrderUI;
		pnj.PlayerTakeOrder +=TakeOrderUI;
		pnj.ClientWhantTheCheck += CheckUI;
		pnj.PlayerTakeTheCheck += TakeTheCheck;
		pnj.ClientWhait += WhaitOrder;
		pnj.PlayerGiveTheOrder += GiveTheOrder;
		
		
		whantOrder.transform.DOScale(0, 0.2f);
		checkUI.transform.DOScale(0, 0.2f);
		
		
		whaitOrder.SetActive(false);
	}
	private void OrderUI(PnjMove obj)
	{
		whantOrder.transform.DOScale(0.05f, 0.4f);
	}
	private void TakeOrderUI(PnjMove obj)
	{
		whantOrder.SetActive(false);
	}
	
	private void WhaitOrder(PnjMove obj)
	{
		whaitOrder.SetActive(true);
	}
	private void GiveTheOrder(PnjMove obj)
	{
		whaitOrder.SetActive(false);
	}
	private void CheckUI(PnjMove obj)
	{
		if (isActived)
		{
			isActived = false;
			whaitOrder.SetActive(false);
			Debug.Log("hiiii");
			checkUI.SetActive(true);
			checkUI.transform.DOScale(0.1f, 0.4f);
		}
	}
	private void TakeTheCheck(PnjMove obj)
	{
		checkUI.transform.DOScale(0, 0.4f);
	}
	
}
