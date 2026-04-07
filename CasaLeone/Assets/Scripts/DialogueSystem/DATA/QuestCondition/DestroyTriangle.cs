using UnityEngine;

namespace DialogueSystem.DATA.QuestCondition
{
    public class DestroyTriangle : MonoBehaviour
    {
        private string conditionID = "Pizza";
 
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
 
            ConditionManager.SetCondition("Pizza", true);
            Destroy(gameObject);
        }
    }
}
