using DialogueSystem.DATA;
using UnityEngine;

namespace DialogueSystem.Runtime
{
    public class WorldSpaceDialogueUI : Singleton<WorldSpaceDialogueUI>
    {
        [Header("Prefab de la bulle")]
        public GameObject bubblePrefab;

        [Header("Décalage local au-dessus du NPC")]
        public Vector3 bubbleOffset = new Vector3(2f, 4f, 0f);

        private WorldSpaceBubble _currentBubble;
        
        public void ShowBubble(Transform npcTransform)
        {
            HideBubble();

            GameObject go = Instantiate(
                bubblePrefab,
                npcTransform.position + bubbleOffset,
                Quaternion.identity,
                npcTransform  // ← enfant du NPC
            );
            go.transform.localPosition = bubbleOffset;

            _currentBubble = go.GetComponent<WorldSpaceBubble>();
            if (_currentBubble == null)
            {
                Destroy(go);
                return;
            }
        }

        public void HideBubble()
        {
            if (_currentBubble != null)
            {
                Destroy(_currentBubble.gameObject);
                _currentBubble = null;
            }
        }

        public void Display(DialogueNode node)
        {
            _currentBubble?.Display(node);
        }

        public bool OnPlayerAdvance() => _currentBubble != null && _currentBubble.OnPlayerAdvance();

        public bool IsActive => _currentBubble != null;
    }
}