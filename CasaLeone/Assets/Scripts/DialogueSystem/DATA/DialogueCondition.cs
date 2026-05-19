using UnityEngine;

namespace DialogueSystem.DATA
{
    [CreateAssetMenu(fileName = "New Condition", menuName = "Dialogue/Condition")]
    public class DialogueCondition : ScriptableObject
    {
        public string conditionID;

        [Header("Quête")]
        [Tooltip("Nom affiché dans le carnet de quêtes")]
        public string questName;
        [TextArea(2, 4)]
        public string description;
    }
}