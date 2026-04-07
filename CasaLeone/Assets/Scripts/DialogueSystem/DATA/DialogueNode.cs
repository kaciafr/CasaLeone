using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.DATA
{
    [CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue Node")]
    public class DialogueNode : ScriptableObject
    {
        [Header("Speaker")]
        public string speakerName;
        public Sprite portrait;

        [Header("Sprite Sequence")]
 
        public List<GameObject> spritePrefabs;

        [Header("Choices")]
        public List<DialogueChoice> choices;

        [Header("Auto Next (if no choices)")]
        public DialogueNode nextNode;

        public bool endsConversation;
    }
}