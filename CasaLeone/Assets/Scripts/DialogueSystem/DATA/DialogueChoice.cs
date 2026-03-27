using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueChoice
{
    public List<GameObject> choiceSpritePrefabs;
    
    public DialogueNode nextNode;
    public DialogueCondition requiredCondition;

    [Header("Optional Condition")]
    public bool requiresCondition;
    public string conditionID;
}