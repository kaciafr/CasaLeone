using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

public class ActivateOnNode : MonoBehaviour
{
    [Tooltip("Drag la DialogueNode qui doit activer cet objet")]
    public DialogueNode activateOnNode;

    [Tooltip("Le GameObject enfant à activer (visuel + collider)")]
    public GameObject visual;

    void Awake()
    {
        if (visual != null)
            visual.SetActive(false);

        DialogueManager.onNodeDisplayed += OnNodeDisplayed;
    }

    void OnDestroy()
    {
        DialogueManager.onNodeDisplayed -= OnNodeDisplayed;
    }

    void OnNodeDisplayed(DialogueNode node)
    {
        if (node != activateOnNode) return;
        if (visual != null)
            visual.SetActive(true);

        DialogueManager.onNodeDisplayed -= OnNodeDisplayed;
    }
}