using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

namespace DialogueSystem
{
    public class NPCTriggerDialogue : MonoBehaviour
    {
        private DialogueTrigger dialogueTrigger;
        private bool playerInRange;

        [Header("Servir")]
        public string requiredConditionID = "";
        public bool resetConditionAfter = true;

        void Start()
        {
            dialogueTrigger = GetComponent<DialogueTrigger>();
        }

        void Update()
        {
            if (!playerInRange) return;
            if (!Input.GetKeyDown(KeyCode.E)) return;

            DialogueConversation conv = dialogueTrigger.conversation;
            if (conv == null) return;

            if (!conv.canRepeat && ConditionManager.CheckCondition(conv.conversationID + "_done"))
                return;

            DialogueManager.Instance.StartConversation(conv, requiredConditionID, OnThisConversationEnded);
        }

        void OnThisConversationEnded()
        {
            if (resetConditionAfter && requiredConditionID != "")
            {
                ConditionManager.SetCondition(requiredConditionID, false);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) playerInRange = true;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player")) playerInRange = false;
        }
    }
}