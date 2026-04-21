using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

namespace DialogueSystem
{
    public class NPCTriggerDialogue : MonoBehaviour
    {
        private DialogueTrigger _dialogueTrigger;
        private bool            _playerInRange;

        [Header("Condition déclenchée à la fin (optionnel)")]
        public string triggerConditionOnEnd = "";

        void Start()
        {
            _dialogueTrigger = GetComponent<DialogueTrigger>();
        }

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;

            if (DialogueManager.Instance.IsInConversation)
            {
                DialogueManager.Instance.PlayerAdvance();
                return;
            }

            if (!_playerInRange) return;

            DialogueConversation conv = _dialogueTrigger?.conversation;
            if (conv == null) return;

            if (!conv.canRepeat && ConditionManager.CheckCondition(conv.conversationID + "_done"))
                return;

            StartDialogue(conv);
        }

        // ── Déclenchement par script externe ─────────────────────────────────

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

        // ── Privé ─────────────────────────────────────────────────────────────

        void StartDialogue(DialogueConversation conversation)
        {
            DialogueManager.Instance.StartConversation(conversation, transform, "", OnConversationEnded);
        }

        void OnConversationEnded()
        {
            if (!string.IsNullOrEmpty(triggerConditionOnEnd))
                ConditionManager.SetCondition(triggerConditionOnEnd, true);
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) _playerInRange = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) _playerInRange = false;
        }
    }
}