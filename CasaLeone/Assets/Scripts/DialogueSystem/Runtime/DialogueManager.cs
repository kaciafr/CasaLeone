using System;
using DialogueSystem.DATA;
using UnityEngine;

namespace DialogueSystem.Runtime
{
    public class DialogueManager : MonoBehaviour
    {
        public static event Action<DialogueNode> onNodeDisplayed;

        public static DialogueManager Instance;

        [Header("UI 3D")]
        public WorldSpaceDialogueUI worldSpaceUI;

        [Header("Conversations à reset au démarrage")]
        public DialogueConversation[] conversationsToReset;

        public DialogueNode CurrentNode => _currentNode;

        private DialogueNode         _currentNode;
        private DialogueConversation _currentConversation;
        private Action               _onEndedCallback;

        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        void Start()
        {
            foreach (var conv in conversationsToReset)
                if (conv != null) conv.conditionReachedNode = null;
        }

        public void StartConversation(DialogueConversation conversation, Transform npcTransform,
                                      string conditionID = "", Action onEnded = null)
        {
            _currentConversation = conversation;
            _onEndedCallback     = onEnded;
            _currentNode         = conversation.GetEntryNode();

            worldSpaceUI.ShowBubble(npcTransform);
            DisplayNode(_currentNode);
        }

        public void PlayerAdvance()
        {
            bool canGoNext = worldSpaceUI.OnPlayerAdvance();
            if (canGoNext) Next();
        }

        public void Next()
        {
            // Si on vient de jouer nodeAfterCondition (le merci) →
            // on mémorise nodeAfterConditionPlayed comme nouvelle starting node
            // pour que le merci ne se rejoue plus jamais
            if (_currentNode == _currentConversation.nodeAfterCondition &&
                _currentConversation.nodeAfterConditionPlayed != null)
            {
                _currentConversation.conditionReachedNode = _currentConversation.nodeAfterConditionPlayed;
            }

            DialogueNode nextNode = _currentNode.ResolveNextNode();

            if (nextNode != null)
            {
                // Si branche conditionnelle → mémorise comme nouvelle entry node
                if (IsConditionalBranch(_currentNode, nextNode))
                    _currentConversation.conditionReachedNode = nextNode;

                _currentNode = nextNode;
                DisplayNode(_currentNode);
            }
            else
            {
                EndConversation();
            }
        }

        public bool IsInConversation => worldSpaceUI.IsActive;

        // ── Privé ──────────────────────────────────────────────────────────────

        void DisplayNode(DialogueNode node)
        {
            worldSpaceUI.Display(node);
            onNodeDisplayed?.Invoke(node);
        }

        bool IsConditionalBranch(DialogueNode current, DialogueNode next)
        {
            if (current.conditionalBranches == null) return false;
            foreach (var branch in current.conditionalBranches)
                if (branch.branchNode == next) return true;
            return false;
        }

        void EndConversation()
        {
            // Si la conversation se termine sur nodeAfterCondition (merci joué jusqu'à la fin)
            // → mémorise nodeAfterConditionPlayed
            if (_currentConversation.nodeAfterCondition != null &&
                _currentConversation.nodeAfterConditionPlayed != null &&
                _currentConversation.conditionReachedNode == null)
            {
                if (ConditionManager.CheckCondition(_currentConversation.conditionID))
                    _currentConversation.conditionReachedNode = _currentConversation.nodeAfterConditionPlayed;
            }

            if (_currentConversation != null && !_currentConversation.canRepeat)
                ConditionManager.SetCondition(_currentConversation.conversationID + "_done", true);

            worldSpaceUI.HideBubble();

            Action callback      = _onEndedCallback;
            _currentConversation = null;
            _currentNode         = null;
            _onEndedCallback     = null;

            callback?.Invoke();
        }
    }
}