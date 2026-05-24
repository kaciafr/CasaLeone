using System;
using Players;
using Players.Inventories;
using Restaurants;
using Restaurants.QTESysteme;
using UnityEngine;

public class Cooker : MonoBehaviour,IQteListen
{
	public int maxSequence = 5;
	public int minSequence = 5;
	public int round = 3;
	public QTESysteme qteSysteme;
	public Dish winGift;
	public event Action<Dish> showFood;
	
	[SerializeField] private InventoryManager inventoryManager;
	public void OnQteStart()
	{
		showFood?.Invoke(winGift);
		
		if (winGift != null)
		{
			qteSysteme.maxSequence = maxSequence;
			qteSysteme.minSequence = minSequence;
			qteSysteme.GenerateSequence();
		}
	}

	public void OnQteSucces(GlobalPlayer globalPlayer)
	{
		if (winGift != null)
		{
			Debug.Log("ZEUBi");
			inventoryManager.GlobalInventory.AddDish(winGift);
			winGift = null;
		}
	}

	public void OnQteFail()
	{
		winGift = null;
		Restaurant.Instance.AddOrRemoveStress(4);
	}

	public int QteRound() => round;

}
