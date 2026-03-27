using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Condition Database", menuName = "Dialogue/Condition Database")]
public class ConditionDatabase : ScriptableObject
{
    public List<DialogueCondition> conditions = new List<DialogueCondition>();
}