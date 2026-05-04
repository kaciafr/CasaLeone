using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using Players;
using Players.Interaction;
using UnityEngine;

namespace DialogueSystem
{
    public class NPCTriggerDialogue : MonoBehaviour, IInteractable
    {
        private DialogueTrigger _dialogueTrigger;

        [Header("Condition déclenchée à la fin (optionnel)")]
        public string triggerConditionOnEnd = "";

        void Start()
        {
            _dialogueTrigger = GetComponent<DialogueTrigger>();
        }


        public void Interact(GlobalPlayer globalPlayer)
        {
            if (DialogueManager.Instance.IsInConversation)
            {
                DialogueManager.Instance.PlayerAdvance();
                return;
            }

            DialogueConversation conv = _dialogueTrigger?.conversation;
            if (conv == null) return;

            if (!conv.canRepeat && ConditionManager.CheckCondition(conv.conversationID + "_done"))
                return;

            StartDialogue(conv);
        }


        public void TriggerDialogue(DialogueConversation conversation)
        {
            if (conversation == null) return;
            if (DialogueManager.Instance.IsInConversation) return;
            StartDialogue(conversation);
        }

        public void TriggerDialogue(DialogueConversation conversation, System.Action onEnded)
        {
            if (conversation == null) return;
            if (DialogueManager.Instance.IsInConversation) return;
            DialogueManager.Instance.StartConversation(conversation, transform, "", onEnded);
        }


        void StartDialogue(DialogueConversation conversation)
        {
            DialogueManager.Instance.StartConversation(conversation, transform, "", OnConversationEnded);
        }

        void OnConversationEnded()
        {
            if (!string.IsNullOrEmpty(triggerConditionOnEnd))
                ConditionManager.SetCondition(triggerConditionOnEnd, true);
        }
    }
}