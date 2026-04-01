using UnityEngine;
using UnityEngine.InputSystem;

public class SphereInteract : MonoBehaviour
{
	private IInteract current;

	public void OnInteractInput(InputAction.CallbackContext context)
	{
		if (current != null && context.performed)
		{
			current.Interact();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IInteract interactable))
		{
			current = interactable;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out IInteract interactable))
		{
			if (current == interactable)
			{
				current.EndInteraction();
				current = null;
			}
		}
	}
}
