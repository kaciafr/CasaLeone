using Restaurants;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

namespace LioneManager.State
{
    public class SlowZone : MonoBehaviour
    {
        [SerializeField] private Restaurant _restaurant;
        
        private List<PlayerMovement> _playersInZone = new List<PlayerMovement>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerMovement playerMovement = other.GetComponentInParent<PlayerMovement>();

                if (playerMovement != null && !_playersInZone.Contains(playerMovement))
                {
                    _playersInZone.Add(playerMovement);
                    _restaurant.AddOrRemoveStress(5);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerMovement playerMovement = other.GetComponentInParent<PlayerMovement>();

                if (playerMovement != null && _playersInZone.Contains(playerMovement))
                {
                    playerMovement.ResetSpeed();
                    _playersInZone.Remove(playerMovement);
                }
            }
        }

        private void Update()
        {
            float multiplier = GetMultiplierFromStress();
            foreach (PlayerMovement player in _playersInZone)
            {
                player.ApplyMultiplier(multiplier); // 
            }
        }


        private float GetMultiplierFromStress()
        {
            IStressBar currentState = _restaurant.currentStressBar;
            switch (currentState)
            {
                case NormalState:        return 0.9f;
                case LightStressState:   return 0.6f;
                case HightStressState:   return 0.4f;
                case MadMaxSStressState: return 0.1f;
                default:                 return 1f;
            }
        }
    }
}
