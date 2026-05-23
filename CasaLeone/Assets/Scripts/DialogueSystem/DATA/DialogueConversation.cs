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
        [Tooltip("ID de la condition validée quand le joueur rend l'objet (ex: 'Lunettes')")]
        public string conditionID;

        [Tooltip("Node jouée UNE SEULE FOIS quand l'objet est rendu (remerciement)")]
        public DialogueNode nodeAfterCondition;

        [Tooltip("Node de redépart APRES que le remerciement ait été joué une fois")]
        public DialogueNode nodeAfterConditionPlayed;

        [HideInInspector]
        public bool itemCollected = false;

       
        [HideInInspector]
        public DialogueNode conditionReachedNode;

        public DialogueNode GetEntryNode()
        {
            Debug.Log($"{name} — conditionReachedNode={conditionReachedNode?.name ?? "null"}, itemCollected={itemCollected}");
    
            if (conditionReachedNode != null)
                return conditionReachedNode;

            if (itemCollected && nodeAfterCondition != null)
                return nodeAfterCondition;

            return startingNode;
        }

        public void ResetItemCollected()
        {
            itemCollected = false;
        }
    }
}