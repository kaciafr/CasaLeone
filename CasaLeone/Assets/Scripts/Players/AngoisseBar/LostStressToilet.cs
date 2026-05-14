using System;
using Restaurants;
using UnityEngine;

public class LostStressToilet : MonoBehaviour
{
   [SerializeField] private float lostStress;
   private float currentLostStress;
   [SerializeField] private float timeToStayIn = 30f;

   private void Start()
   {
      currentLostStress = lostStress;
   }
   private void OnTriggerStay(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         timeToStayIn-= Time.deltaTime;
         if (timeToStayIn >= 15)
            Restaurant.Instance.AddOrRemoveStress(-currentLostStress);

         if (timeToStayIn <= 0)
         {
            Debug.Log("Stress Reload is empty");
            currentLostStress = 0;
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other == null)
      {
         timeToStayIn += Time.deltaTime;
         if (timeToStayIn >= 30)
         {
            Debug.Log("Stress Reload is full");
            timeToStayIn = 30;
            currentLostStress = lostStress;
         }
      }
   }
}
