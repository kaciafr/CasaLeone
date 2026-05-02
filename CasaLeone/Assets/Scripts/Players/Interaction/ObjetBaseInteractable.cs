using System;
using DG.Tweening;
using Restaurants.QTESysteme;
using Restaurants.QTESysteme.UiQte;
using UnityEngine;

namespace Players.Interaction
{
	public class ObjetBaseInteractable :  MonoBehaviour,IInteractable
	{
		[Header("References")]
		[SerializeField] private Transform objectTransform;
		[SerializeField] private GameObject pressE;
		[SerializeField] private GameObject endPosition;
		[SerializeField] private GameObject startPosition;
		[SerializeField] private QTESysteme qteSysteme;
		public UiQte qteUi;
		[SerializeField] private TransformeUiQte UiLocQte;
		
		public GlobalPlayer currentPlayer;

		[SerializeField] private float speedAnim;
		public bool lockDoor = false;

		public Action<Transform> UILoc;
		

		private void Start()
		{
			pressE.transform.position = startPosition.transform.position;
			qteSysteme =  qteSysteme.GetComponent<QTESysteme>();
		}
		public void Interact(GlobalPlayer globalPlayer)
		{
			currentPlayer = globalPlayer;
			pressE.SetActive(false);
			UiLocQte.UiTransform(objectTransform);
			qteSysteme.objetBaseInteractable = this;
			qteSysteme.StartSequence();
		}
		private void OnTriggerEnter(Collider other)
		{
			
			pressE.SetActive(true);
			qteSysteme.enabled = true;
			qteUi.enabled = true;
		}

		private void OnTriggerStay(Collider other)
		{
			pressE.transform.DOMove(endPosition.transform.position, speedAnim);
		}

		private void OnTriggerExit(Collider other)
		{
			currentPlayer = null;
			pressE.transform.DOMove(startPosition.transform.position, speedAnim);
			qteSysteme.enabled = false;
			qteUi.enabled = false;
		}
	
	}
}
