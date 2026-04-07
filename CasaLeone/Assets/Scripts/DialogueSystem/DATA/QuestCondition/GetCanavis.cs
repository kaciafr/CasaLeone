using UnityEngine;

namespace DialogueSystem.DATA.QuestCondition
{
    public class GetCanavis : MonoBehaviour
    {
        private string conditionID = "CatNap";
 
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
 
            ConditionManager.SetCondition(conditionID, true);
            Destroy(gameObject);
        }
    }
}
