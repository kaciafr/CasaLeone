using UnityEngine;

namespace DialogueSystem.DATA.QuestCondition
{
    public class DestroyTriangle : MonoBehaviour
    {
        private string conditionID = "Pizza";
 
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
 
            ConditionManager.SetCondition("Pizza", true);
            Destroy(gameObject);
        }
    }
}
