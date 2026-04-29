using System;
using Restaurants;
using UnityEngine;

public class LostStressToilet : MonoBehaviour
{
   [SerializeField] private float lostStress;

   private void OnTriggerStay(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         Restaurant.Instance.AddOrRemoveStress(-1);
      }
   }
}
