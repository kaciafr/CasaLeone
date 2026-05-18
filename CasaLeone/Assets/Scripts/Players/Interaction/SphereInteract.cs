using System.Collections.Generic;
using Clients;
using Clients.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players.Interaction
{
    public class SphereInteract : MonoBehaviour
    {
       [SerializeField] 
       private GlobalPlayer globalPlayer;

       private List<IInteractable> interactablesInRange = new List<IInteractable>();

       public void OnInteractInput(InputAction.CallbackContext context)
       { 
          if (context.performed)
          {
             IInteractable priorityTarget = GetPriorityInteractable();

             if (priorityTarget != null)
             {
                priorityTarget.Interact(globalPlayer);
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
             if (interactable.Priotity > maxPriority)
             {
                maxPriority = interactable.Priotity;
                highestPriorityTarget = interactable;
             }
          }

          return highestPriorityTarget;
       }
    }
}