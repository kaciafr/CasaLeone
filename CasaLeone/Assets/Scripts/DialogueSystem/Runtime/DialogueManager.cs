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

        public DialogueNode CurrentNode => _currentNode;

        private DialogueNode         _currentNode;
        private DialogueConversation _currentConversation;
        private Action               _onEndedCallback;

        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void StartConversation(DialogueConversation conversation, Transform npcTransform,
                                      string conditionID = "", Action onEnded = null)
        {
            _currentConversation = conversation;
            _onEndedCallback     = onEnded;

            _currentNode = ResolveEntryNode(conversation.startingNode);

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
            DialogueNode nextNode = _currentNode.ResolveNextNode();

            if (nextNode != null)
            {
                _currentNode = nextNode;
                DisplayNode(_currentNode);
            }
            else
            {
                EndConversation();
            }
        }

        public bool IsInConversation => worldSpaceUI.IsActive;
        
        DialogueNode ResolveEntryNode(DialogueNode startingNode)
        {
            if (startingNode == null) return null;

            DialogueNode resolved = startingNode.ResolveNextNode();
            
            if (resolved != startingNode.nextNode)
            {
                return resolved ?? startingNode;
            }

            return startingNode;
        }
        void DisplayNode(DialogueNode node)
        {
            worldSpaceUI.Display(node);
            onNodeDisplayed?.Invoke(node);
        }

        void EndConversation()
        {
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