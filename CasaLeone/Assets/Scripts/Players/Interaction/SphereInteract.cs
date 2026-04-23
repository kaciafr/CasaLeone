using Clients;
using Clients.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players.Interaction
{
	public class SphereInteract : MonoBehaviour
	{
		private IInteractable current;

		[SerializeField] 
		private GlobalPlayer globalPlayer;
		
		public void OnInteractInput(InputAction.CallbackContext context)
		{ 
			if (current != null && context.performed)
			{
				current.Interact(globalPlayer);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IInteractable interactable))
			{
				current = interactable;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out IInteractable interactable))
			{
				if (current == interactable)
				{
					current = null;
				}
			}
		}
	}
}
