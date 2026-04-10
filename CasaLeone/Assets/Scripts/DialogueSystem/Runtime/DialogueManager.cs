using System;
using DialogueSystem.DATA;
using UnityEngine;

namespace DialogueSystem.Runtime
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [Header("UI 3D")]
        public WorldSpaceDialogueUI worldSpaceUI;

        // Exposé pour que WorldSpaceBubble puisse skip le typewriter
        public DialogueNode CurrentNode => _currentNode;

        // ── Privé ──────────────────────────────────────────────────────────────
        private DialogueNode         _currentNode;
        private DialogueConversation _currentConversation;
        private Action               _onEndedCallback;
        private string               _requiredConditionID = "";

        // ── Awake ──────────────────────────────────────────────────────────────

        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // ── API publique ───────────────────────────────────────────────────────

        public void StartConversation(DialogueConversation conversation, Transform npcTransform,
                                      string conditionID = "", Action onEnded = null)
        {
            _currentConversation = conversation;
            _currentNode         = conversation.startingNode;
            _onEndedCallback     = onEnded;
            _requiredConditionID = conditionID;

            worldSpaceUI.ShowBubble(npcTransform);
            worldSpaceUI.Display(_currentNode);
        }
        
        public void PlayerAdvance()
        {
            bool canGoNext = worldSpaceUI.OnPlayerAdvance();
            if (canGoNext) Next();
        }

        /// <summary>Passe au nœud suivant (ou termine la conversation).</summary>
        public void Next()
        {
            if (_currentNode.nextNode != null)
            {
                _currentNode = _currentNode.nextNode;
                worldSpaceUI.Display(_currentNode);
            }
            else
            {
                EndConversation();
            }
        }

        public bool IsInConversation => worldSpaceUI.IsActive;

        // ── Privé ──────────────────────────────────────────────────────────────

        void EndConversation()
        {
            if (_currentConversation != null && !_currentConversation.canRepeat)
                DATA.ConditionManager.SetCondition(_currentConversation.conversationID + "_done", true);

            worldSpaceUI.HideBubble();

            Action callback      = _onEndedCallback;
            _currentConversation = null;
            _currentNode         = null;
            _onEndedCallback     = null;
            _requiredConditionID = "";

            callback?.Invoke();
        }
    }
}