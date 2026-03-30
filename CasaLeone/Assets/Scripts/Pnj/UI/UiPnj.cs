using DG.Tweening;
using UnityEngine;

public class UiPnj : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private pnjMove pnj;

	[Header("UI Elements")] 
	[SerializeField] private GameObject whantOrder;
	[SerializeField] private GameObject whaitOrder;

	private void Start()
	{
		pnj.ClientWhantTakeOrder += OrderUI;
		pnj.ClientWhait += WhaitOrder;
		whantOrder.transform.DOScale(0, 0.2f);
		whaitOrder.SetActive(false);
	}
	
	private void OrderUI(pnjMove obj)
	{
		whantOrder.transform.DOScale(1, 0.2f);
	}
	
	private void WhaitOrder(pnjMove obj)
	{
		whantOrder.SetActive(false);
		whaitOrder.SetActive(true);
	}
	
}
