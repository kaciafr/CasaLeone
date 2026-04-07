using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.DATA
{
    [CreateAssetMenu(fileName = "Condition Database", menuName = "Dialogue/Condition Database")]
    public class ConditionDatabase : ScriptableObject
    {
        public List<DialogueCondition> conditions = new List<DialogueCondition>();
    }
}