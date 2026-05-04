using DG.Tweening;
using Players;
using Restaurants.QTESysteme;
using UnityEngine;

public class LockDoor : MonoBehaviour , IQteListen
{
	public int maxSequence;
	public int minSequence;
	
	[SerializeField] private SpriteRenderer hideRoom;
	[SerializeField] private Transform doorPivot;
	[SerializeField] private QTESysteme qteSysteme;
	public Collider door;

	
	public void OnQteStart()
	{
		qteSysteme.maxSequence = maxSequence;
		qteSysteme.minSequence = minSequence;
		qteSysteme.GenerateSequence();
	}

	public void OnQteSucces(GlobalPlayer globalPlayer)
	{
		Debug.Log("PORTE DEVERROUILLEE");
		door.enabled = false;
		doorPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
		
		
		hideRoom.DOFade(0, 3f);
		
	}

	public void OnQteFail()
	{
	}

	public int QteRound() => 0;

}
