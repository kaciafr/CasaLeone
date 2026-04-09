using DG.Tweening;
using UnityEngine;

namespace Clients.UI
{
	public class UiPnj : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private ClientMovement client;

		[Header("UI Elements")] 
		[SerializeField] private GameObject whantOrder;
		[SerializeField] private GameObject whaitOrder;
		[SerializeField] private GameObject checkUI;
		private bool isActived = true;

		private void Start()
		{
			client.ClientWhantTakeOrder += OrderUI;
			client.PlayerTakeOrder +=TakeOrderUI;
			client.ClientWhantTheCheck += CheckUI;
			client.PlayerTakeTheCheck += TakeTheCheck;
			client.ClientWhait += WhaitOrder;
			client.PlayerGiveTheOrder += GiveTheOrder;
		
		
			whantOrder.transform.DOScale(0, 0.2f);
			checkUI.transform.DOScale(0, 0.2f);
		
		
			whaitOrder.SetActive(false);
		}
		private void OrderUI(ClientMovement obj)
		{
			whantOrder.transform.DOScale(0.05f, 0.4f);
		}
		private void TakeOrderUI(ClientMovement obj)
		{
			whantOrder.SetActive(false);
		}
	
		private void WhaitOrder(ClientMovement obj)
		{
			whaitOrder.SetActive(true);
		}
		private void GiveTheOrder(ClientMovement obj)
		{
			whaitOrder.SetActive(false);
		}
		private void CheckUI(ClientMovement obj)
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
		private void TakeTheCheck(ClientMovement obj)
		{
			checkUI.transform.DOScale(0, 0.4f);
		}
	
	}
}
