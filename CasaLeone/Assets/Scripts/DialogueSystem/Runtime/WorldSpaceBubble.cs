using System.Collections;
using TMPro;
using UnityEngine;

namespace DialogueSystem.Runtime
{
    public class WorldSpaceBubble : MonoBehaviour
    {
        [Header("Références (dans le prefab)")]
        public TextMeshPro dialogueLabel;

        private Coroutine _typeCoroutine;
        private float typeSpeed = 0.04f;
        private Coroutine _autoAdvanceCoroutine;
        private bool      _textComplete = false;
        

        public void Display(DATA.DialogueNode node)
        {
            _textComplete = false;

            if (dialogueLabel != null) dialogueLabel.text = "";

            if (_typeCoroutine        != null) StopCoroutine(_typeCoroutine);
            if (_autoAdvanceCoroutine != null) StopCoroutine(_autoAdvanceCoroutine);

            _typeCoroutine = StartCoroutine(TypeText(node.dialogueText, () =>
            {
                _textComplete = true;

                if (node.autoAdvanceDelay > 0f)
                    _autoAdvanceCoroutine = StartCoroutine(AutoAdvance(node.autoAdvanceDelay));
            }));
        }
        
        public bool OnPlayerAdvance()
        {
            if (!_textComplete)
            {
                SkipTypewriter();
                return false; 
            }
            return true;
        }
        IEnumerator TypeText(string text, System.Action onComplete)
        {
            if (dialogueLabel == null) yield break;
            dialogueLabel.text = "";
            foreach (char c in text)
            {
                dialogueLabel.text += c;
                yield return new WaitForSeconds(typeSpeed);
            }
            _typeCoroutine = null;
            onComplete?.Invoke();
        }

        void SkipTypewriter()
        {
            if (_typeCoroutine != null)
            {
                StopCoroutine(_typeCoroutine);
                _typeCoroutine = null;
            }
            var node = DialogueManager.Instance.CurrentNode;
            if (node != null && dialogueLabel != null)
                dialogueLabel.text = node.dialogueText;

            _textComplete = true;

            if (node != null && node.autoAdvanceDelay > 0f)
                _autoAdvanceCoroutine = StartCoroutine(AutoAdvance(node.autoAdvanceDelay));
        }

        IEnumerator AutoAdvance(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (DialogueManager.Instance != null && DialogueManager.Instance.CurrentNode != null)
                DialogueManager.Instance.Next();
        }

    }
}