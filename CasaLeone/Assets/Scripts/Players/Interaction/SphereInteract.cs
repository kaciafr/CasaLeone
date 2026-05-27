using System;
using System.Collections.Generic;
using Clients;
using Clients.States;
using Sound;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players.Interaction
{
	public class SphereInteract : MonoBehaviour
	{
		[SerializeField] private GlobalPlayer globalPlayer;

		private HashSet<IInteractable> interactablesInRange = new ();

		private HashSet<IInteractable> insideTrigger = new ();

		public void OnInteractInput(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				IInteractable priorityTarget = GetPriorityInteractable();

				if (priorityTarget != null)
				{
					SoundManager.Instance.StopSFX();
					SoundManager.Instance.PlaySFX(SoundType.Interact);
					priorityTarget.Interact(globalPlayer);
					Debug.Log(priorityTarget);
				}
				else
				{
					SoundManager.Instance.StopSFX();
					SoundManager.Instance.PlaySFX(SoundType.QteFail);
				}
			}
		}

		private void FixedUpdate()
		{
			foreach (var interactable in interactablesInRange)
			{
				if(!insideTrigger.Contains(interactable))
					interactable.OnPlayerExit(globalPlayer);
			}

			foreach (var interactable in insideTrigger)
			{
				if(!interactablesInRange.Contains(interactable))
					interactable.OnPlayerEnter(globalPlayer);
			}
			
			interactablesInRange.Clear();
			
			foreach (var interactable in insideTrigger)
				interactablesInRange.Add(interactable);
				
			insideTrigger.Clear();
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.TryGetComponent(out IInteractable interactable))
			{
				insideTrigger.Add(interactable);
			}
		}

		private IInteractable GetPriorityInteractable()
		{
			if (interactablesInRange == null || interactablesInRange.Count == 0) return null;

			IInteractable highestPriorityTarget = null;
			int maxPriority = int.MinValue;

			foreach (var interactable in interactablesInRange)
			{
				if (interactable.Priority > maxPriority)
				{
					maxPriority = interactable.Priority;
					highestPriorityTarget = interactable;
				}
			}

			return highestPriorityTarget;
		}
	}
}