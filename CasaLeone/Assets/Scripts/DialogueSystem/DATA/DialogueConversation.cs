using UnityEngine;

namespace DialogueSystem.DATA
{
    [CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/Dialogue Conversation")]
    public class DialogueConversation : ScriptableObject
    {
        public string       conversationID;
        public DialogueNode startingNode;
        public bool         canRepeat = true;

        [HideInInspector]
        public DialogueNode conditionReachedNode;

        public DialogueNode GetEntryNode()
        {
            Debug.Log($"conditionReachedNode = {conditionReachedNode?.name ?? "null"}");
            return conditionReachedNode != null ? conditionReachedNode : startingNode;
        }
    }
}