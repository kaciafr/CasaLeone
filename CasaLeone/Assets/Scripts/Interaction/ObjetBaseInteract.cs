using DG.Tweening;
using UnityEngine;

public class ObjetBaseInteract :  MonoBehaviour,IInteract
{
	[SerializeField] private GameObject pressE;
	[SerializeField] private GameObject endPosition;
	[SerializeField] private GameObject startPosition;

	[SerializeField] private float speedAnim;

	private void Start()
	{
		pressE.transform.position = startPosition.transform.position;
	}
	public void Interact()
	{
		Debug.Log("ObjetBaseInteract");
	}

	private void OnTriggerStay(Collider other)
	{
		pressE.transform.DOMove(endPosition.transform.position, speedAnim);
	}

	private void OnTriggerExit(Collider other)
	{
		pressE.transform.DOMove(startPosition.transform.position, speedAnim);
	}

	public void EndInteraction()
	{
		
	}
	
}
