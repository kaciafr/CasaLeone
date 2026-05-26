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
       [SerializeField] 
       private GlobalPlayer globalPlayer;

       public List<IInteractable> interactablesInRange = new List<IInteractable>();

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

       private void OnTriggerEnter(Collider other)
       {
          if (other.TryGetComponent(out IInteractable interactable))
          {
             if (!interactablesInRange.Contains(interactable))
             {
                interactablesInRange.Add(interactable);
             }
          }
       }

       private void OnTriggerExit(Collider other)
       {
          if (other.TryGetComponent(out IInteractable interactable))
          {
             if (interactablesInRange.Contains(interactable))
             {
                interactablesInRange.Remove(interactable);
                interactablesInRange.Clear();
             }
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