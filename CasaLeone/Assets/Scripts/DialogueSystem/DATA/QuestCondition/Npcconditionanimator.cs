using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPCConditionAnimator : MonoBehaviour
{
    [Tooltip("Drag la node de remerciement qui déclenche le changement d'animation")]
    public DialogueNode thankYouNode;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        DialogueManager.onNodeDisplayed += OnNodeDisplayed;
    }

    void OnDisable()
    {
        DialogueManager.onNodeDisplayed -= OnNodeDisplayed;
    }

    void OnNodeDisplayed(DialogueNode node)
    {
        if (node != thankYouNode) return;

        _animator.SetBool("HasCondition", true);

        DialogueManager.onNodeDisplayed -= OnNodeDisplayed;
    }
}