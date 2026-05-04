using System;
using DG.Tweening;
using Restaurants.QTESysteme;
using Restaurants.QTESysteme.UiQte;
using UnityEngine;

namespace Players.Interaction
{
	public class ObjetBaseInteractableQte :  MonoBehaviour,IInteractableQTE
	{
		[Header("UI")]
		[SerializeField] private Transform objectTransform;
		[SerializeField] private GameObject pressE;
		[SerializeField] private GameObject endPosition;
		[SerializeField] private GameObject startPosition;
		[SerializeField] private QTESysteme qteSysteme;
		[SerializeField] private TransformeUiQte UiLocQte;
		[SerializeField] private float speedAnim;
		public GlobalPlayer playerInventory;
		
		private IQteListen currentListener;
		
		[Header("References")]
		public UiQte qteUi;
		public GlobalPlayer currentPlayer;

		private void Start()
		{
			currentListener = GetComponent<IQteListen>();
			pressE.transform.position = startPosition.transform.position;
			qteSysteme =  qteSysteme.GetComponent<QTESysteme>();
		}
		public void Interact(GlobalPlayer globalPlayer)
		{
			playerInventory = globalPlayer;
			Debug.Log(currentListener);
			currentPlayer = globalPlayer;
			
			pressE.SetActive(false);
			UiLocQte.UiTransform(objectTransform);
			qteSysteme.interactObj = this;
			qteSysteme.StartSequence(currentListener);
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
			playerInventory = null;
			currentPlayer = null;
			pressE.transform.DOMove(startPosition.transform.position, speedAnim);
			qteSysteme.enabled = false;
			qteUi.enabled = false;
		}
	
	}
}
