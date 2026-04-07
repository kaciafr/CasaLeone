using UnityEngine;

namespace DialogueSystem.DATA
{
    [CreateAssetMenu(fileName = "New Condition", menuName = "Dialogue/Condition")]
    public class DialogueCondition : ScriptableObject
    {
        public string conditionID;
        public string description;
    }
}