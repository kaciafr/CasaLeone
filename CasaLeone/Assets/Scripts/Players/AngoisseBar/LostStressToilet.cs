using System;
using System.Collections.Generic;
using Restaurants;
using UnityEngine;

public class LostStressToilet : MonoBehaviour
{
   [SerializeField] private float lostStress;
   private float currentLostStress;
   [SerializeField] private float timeToStayIn = 30f;
   private bool isIn;
   
   public event Action StressIsEmpty;
   public event Action StressIsFull;
   public event Action Exit;
   private void Start()
   {
      currentLostStress = lostStress;
      timeToStayIn = 30f;
   }

   private void Update()
   {
      if (!isIn)
      {
         timeToStayIn += Time.deltaTime;
         if (timeToStayIn >= 30)
         {
            Debug.Log("Stress Reload is full");
            timeToStayIn = 30;
            currentLostStress = lostStress;
            StressIsFull?.Invoke();
         }
      }
   }
   private void OnTriggerStay(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         isIn =  true;
         timeToStayIn-= Time.deltaTime;
         timeToStayIn = Mathf.Clamp(timeToStayIn, 0, timeToStayIn);
         
         if (timeToStayIn >= 15)
            Restaurant.Instance.AddOrRemoveStress(-currentLostStress);

         if (timeToStayIn <= 0)
         {
            Debug.Log("Stress Reload is empty");
            currentLostStress = 0;
            StressIsEmpty?.Invoke();
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         Debug.Log("Stress Reload");
         isIn = false;
         
      }
      if (timeToStayIn <= 0)
      {
         Exit?.Invoke();
      }
   }
}
