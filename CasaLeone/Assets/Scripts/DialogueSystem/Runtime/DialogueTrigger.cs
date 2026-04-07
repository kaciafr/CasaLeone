using DialogueSystem.DATA;
using UnityEngine;

namespace DialogueSystem.Runtime
{
    public class DialogueTrigger : MonoBehaviour
    {
        public DialogueConversation conversation;

        public void TriggerDialogue()
        {
            DialogueManager.Instance.StartConversation(conversation);
        }
    }
}