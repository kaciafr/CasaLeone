using System.Collections;
using TMPro;
using UnityEngine;

namespace DialogueSystem.Runtime
{
    public class WorldSpaceBubble : MonoBehaviour
    {
        [Header("Références (dans le prefab)")]
        public TextMeshPro dialogueLabel;

        [Header("Anti-mur")]
        [Tooltip("Layers de tes murs — PAS le layer du NPC ni de la bulle")]
        public LayerMask wallLayers;
        [Tooltip("Distance de décalage vers la caméra si la bulle est dans un mur")]
        public float wallPushDistance = 0.3f;

        // ── Privé ──────────────────────────────────────────────────────────────
        private Camera    _cam;
        private Transform _npcTransform;
        private Vector3   _offset;

        private Coroutine _typeCoroutine;
        private Coroutine _autoAdvanceCoroutine;
        private float     typeSpeed     = 0.04f;
        private bool      _textComplete = false;

        // ── Init ───────────────────────────────────────────────────────────────

        void Awake()
        {
            _cam = Camera.main;
        }

        void LateUpdate()
        {
            if (_cam == null) return;

            // Billboard
            Vector3 dir = transform.position - _cam.transform.position;
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(dir);

            // Compenser le flip UNIQUEMENT sur le texte
            Transform p = transform.parent;
            if (p != null && dialogueLabel != null)
            {
                float scaleX = p.lossyScale.x != 0 ? 1f / p.lossyScale.x : 1f;
                dialogueLabel.transform.localScale = new Vector3(
                    scaleX,
                    dialogueLabel.transform.localScale.y,
                    dialogueLabel.transform.localScale.z
                );
            }
        }
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

        // ── Privé ──────────────────────────────────────────────────────────────

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