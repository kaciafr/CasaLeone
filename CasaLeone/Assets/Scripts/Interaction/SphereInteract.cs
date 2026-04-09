using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
	public class SphereInteract : MonoBehaviour
	{
		private IInteractable current;

		public void OnInteractInput(InputAction.CallbackContext context)
		{
			if (current != null && context.performed)
			{
				current.Interact();
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
					current.EndInteraction();
					current = null;
				}
			}
		}
	}
}
