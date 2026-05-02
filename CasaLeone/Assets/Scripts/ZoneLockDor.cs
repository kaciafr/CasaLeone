using Players;
using Players.Interaction;
using Restaurants.QTESysteme;
using Restaurants.QTESysteme.UiQte;
using UnityEngine;

namespace DefaultNamespace
{
	public class ZoneLockDor : MonoBehaviour , IInteractable
	{
		public GlobalPlayer currentPlayer;
		[SerializeField] private Transform objectTransform;
		[SerializeField] private QteLockDoor qteSysteme;
		[SerializeField] private UiQte qteUi;
		[SerializeField] private TransformeUiQte UiLocQte;
		
		public void Interact(GlobalPlayer globalPlayer)
		{
			currentPlayer = globalPlayer;
			UiLocQte.UiTransform(objectTransform);
			qteSysteme.StartSequence();
		}
		
		private void OnTriggerEnter(Collider other)
		{
			
			qteSysteme.enabled = true;
			qteUi.enabled = true;
		}

		private void OnTriggerStay(Collider other)
		{
			
		}

		private void OnTriggerExit(Collider other)
		{
			currentPlayer = null;
			qteSysteme.enabled = false;
			qteUi.enabled = false;
		}
	}
}