using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.DATA
{
    [CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue Node")]
    public class DialogueNode : ScriptableObject
    {
        [Header("Dialogue Text")]
        [TextArea(2, 5)]
        public string dialogueText;

        public float autoAdvanceDelay = 7f;

        public DialogueNode nextNode;

        [Header("Conditions pour la suite (vide si c'est uen fin de conv)")]
        public List<ConditionalBranch> conditionalBranches;

        public DialogueNode ResolveNextNode()
        {
            if (conditionalBranches != null)
                foreach (var branch in conditionalBranches)
                    if (!string.IsNullOrEmpty(branch.conditionID) &&
                        ConditionManager.CheckCondition(branch.conditionID))
                        return branch.branchNode;

            return nextNode;
        }
    }

    [System.Serializable]
    public class ConditionalBranch
    {
        [Tooltip("ID de la condition a verifier")]
        public string conditionID;

        [Tooltip("Node affiché si la condition est vraie. Vide = fin de conversation.")]
        public DialogueNode branchNode;
    }
}