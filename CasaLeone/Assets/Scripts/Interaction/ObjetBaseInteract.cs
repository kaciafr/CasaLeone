using System;
using DG.Tweening;
using QTESysteme.UiQte;
using UnityEngine;

namespace Interaction
{
	public class ObjetBaseInteract :  MonoBehaviour,IInteract
	{
		[Header("References")]
		[SerializeField] private Transform objectTransform;
		[SerializeField] private GameObject pressE;
		[SerializeField] private GameObject endPosition;
		[SerializeField] private GameObject startPosition;
		[SerializeField] private QTESysteme.QTESysteme qteSysteme;
		[SerializeField] private TransformeUiQte UiLocQte;
		
		

		[SerializeField] private float speedAnim;

		public Action<Transform> UILoc;

		private void Start()
		{
			pressE.transform.position = startPosition.transform.position;
			qteSysteme =  qteSysteme.GetComponent<QTESysteme.QTESysteme>();
		}
		public void Interact()
		{
			Debug.Log("ObjetBaseInteract");
			pressE.SetActive(false);
			UiLocQte.UiTransform(objectTransform);
			
			qteSysteme.StartSequence();
		}
		private void OnTriggerEnter(Collider other)
		{
			pressE.SetActive(true);
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
}
