using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

public class ActivateScriptOnNode : MonoBehaviour
{
    [Tooltip("La node qui active le script")]
    public DialogueNode activateOnNode;

    [Tooltip("Le script à activer (sur ce GameObject ou un enfant)")]
    public MonoBehaviour scriptToActivate;

    void OnEnable()
    {
        DialogueManager.onNodeDisplayed += OnNodeDisplayed;
    }

    void OnDisable()
    {
        DialogueManager.onNodeDisplayed -= OnNodeDisplayed;
    }

    void Start()
    {
        if (scriptToActivate != null)
            scriptToActivate.enabled = false;
    }

    void OnNodeDisplayed(DialogueNode node)
    {
        if (node != activateOnNode) return;

        if (scriptToActivate != null)
            scriptToActivate.enabled = true;

        DialogueManager.onNodeDisplayed -= OnNodeDisplayed;
    }
}