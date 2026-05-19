using UnityEngine;

namespace DialogueSystem.DATA
{
    [CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/Dialogue Conversation")]
    public class DialogueConversation : ScriptableObject
    {
        public string       conversationID;
        public DialogueNode startingNode;
        public bool         canRepeat = true;

        [Header("Condition de redépart")]
        [Tooltip("ID de la condition qui change le point de départ (ex: 'Lunettes')")]
        public string conditionID;

        [Tooltip("Node jouée UNE SEULE FOIS quand la condition est validée (ex: 'Merci pour les lunettes')")]
        public DialogueNode nodeAfterCondition;

        [Tooltip("Node de redépart APRES que nodeAfterCondition ait été jouée une fois")]
        public DialogueNode nodeAfterConditionPlayed;

        /// <summary>
        /// Mémorisée automatiquement quand une branche conditionnelle est suivie
        /// ou quand nodeAfterCondition a été jouée.
        /// Remise à null au démarrage via DialogueManager.conversationsToReset.
        /// </summary>
        [HideInInspector]
        public DialogueNode conditionReachedNode;

        public DialogueNode GetEntryNode()
        {
            // 1. nodeAfterCondition déjà jouée → repart de conditionReachedNode
            if (conditionReachedNode != null)
                return conditionReachedNode;

            // 2. Condition validée → joue nodeAfterCondition une seule fois
            if (!string.IsNullOrEmpty(conditionID) &&
                ConditionManager.CheckCondition(conditionID) &&
                nodeAfterCondition != null)
                return nodeAfterCondition;

            // 3. Début normal
            return startingNode;
        }
    }
}