using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

namespace DialogueSystem
{
    /// <summary>
    /// À attacher sur chaque NPC (PNG 2D dans scène 3D).
    /// 
    /// Comportement :
    ///   - Le joueur entre dans le trigger 3D du NPC
    ///   - Il appuie sur E pour démarrer la conversation
    ///   - Il appuie à nouveau sur E pour avancer nœud par nœud
    ///     (si le typewriter est en cours, le premier E l'accélère)
    ///   - La conversation se termine seule au dernier nœud
    /// 
    /// Conditions : certains NPC peuvent déclencher une condition
    /// (ex: donner une mission) via le callback onEnded.
    /// </summary>
    public class NPCTriggerDialogue : MonoBehaviour
    {
        private DialogueTrigger _dialogueTrigger;
        private bool            _playerInRange;

        [Header("Condition déclenchée à la fin de cette conversation")]
        public string triggerConditionOnEnd = ""; // Laissez vide si pas de condition
        public bool   resetConditionAfter   = false;

        void Start()
        {
            _dialogueTrigger = GetComponent<DialogueTrigger>();
        }

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;

            // Si une conversation est déjà en cours (avec ce NPC ou un autre)
            if (DialogueManager.Instance.IsInConversation)
            {
                DialogueManager.Instance.PlayerAdvance();
                return;
            }

            // Démarrer une nouvelle conversation
            if (!_playerInRange) return;

            DialogueConversation conv = _dialogueTrigger?.conversation;
            if (conv == null) return;

            // Ne pas relancer si déjà terminée et non-répétable
            if (!conv.canRepeat && ConditionManager.CheckCondition(conv.conversationID + "_done"))
                return;

            DialogueManager.Instance.StartConversation(
                conv,
                transform,
                "",
                OnConversationEnded
            );
        }

        void OnConversationEnded()
        {
            if (string.IsNullOrEmpty(triggerConditionOnEnd)) return;

            ConditionManager.SetCondition(triggerConditionOnEnd, true);

            if (resetConditionAfter)
                ConditionManager.SetCondition(triggerConditionOnEnd, false);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) _playerInRange = true;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player")) _playerInRange = false;
        }
    }
}