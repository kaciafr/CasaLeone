using DialogueSystem.DATA;
using UnityEngine;

namespace DialogueSystem.Runtime
{
    public class WorldSpaceDialogueUI : MonoBehaviour
    {
        [Header("Prefab de la bulle")]
        public GameObject bubblePrefab;

        [Header("Décalage local au-dessus du NPC")]
        public Vector3 bubbleOffset = new Vector3(1f, 2.2f, 0f);

        private WorldSpaceBubble _currentBubble;
        
        public void ShowBubble(Transform npcTransform)
        {
            HideBubble();

            GameObject go = Instantiate(
                bubblePrefab,
                npcTransform.position + npcTransform.TransformDirection(bubbleOffset),
                Quaternion.identity,
                npcTransform
            );
            go.transform.localPosition = bubbleOffset;

            _currentBubble = go.GetComponent<WorldSpaceBubble>();
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