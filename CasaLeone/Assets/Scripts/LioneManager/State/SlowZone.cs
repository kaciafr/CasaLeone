using Restaurants;
using Unity.VisualScripting;
using UnityEngine;

namespace LioneManager.State
{
    public class SlowZone :  MonoBehaviour
    {

        public PlayerMovement _playerMovement; 
        [Serialize] private Restaurant _restaurant;
        private bool _playerInZone = false;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                
                _playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                _playerInZone = true;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                 _playerMovement.ResetSpeed();
                 _playerInZone = false;
                 _playerMovement = null;
            }
        }

        private void Update()
        {
            if (_playerInZone || _playerMovement == null)
                return;
            float multiplier = GetMultiplierFromStress();
                _playerMovement.ApplyMultiplier(multiplier);
        }



        private float GetMultiplierFromStress()
        {
            IStressBar currentState = _restaurant.currentStressBar;
            switch (currentState)
            {
                case NormalState: return 0.9f;
                case LightStressState : return 0.6f;
                case HightStressState : return 0.4f;
                case MadMaxSStressState : return 0.1f;
                
                default: return 1f;
            }
        }
    }
}