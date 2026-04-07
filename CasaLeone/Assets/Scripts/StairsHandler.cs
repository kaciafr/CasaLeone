using CharacterController;
using UnityEngine;


    public class StairsHandler : MonoBehaviour
    {
        [SerializeField] private InputManager _input;
        [SerializeField] Collider2D stairs ;
        

        private void Update()
        {
            if (_input.isActivateStairs)
            {
                stairs.enabled = true;
            }
            else if (_input.isDeactivateStairs)
            {
                stairs.enabled = false;
            }
        }
       
    }
