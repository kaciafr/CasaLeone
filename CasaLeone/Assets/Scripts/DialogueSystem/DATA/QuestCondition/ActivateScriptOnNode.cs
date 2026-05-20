using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

public class ActivateScriptOnNode : MonoBehaviour
{
    [Tooltip("La node qui active le script")]
    public DialogueNode activateOnNode;

    [Tooltip("Le script à activer (sur ce GameObject ou un enfant)")]
    public PnjZoneStress pnjZoneStress;

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
        if (node != activateOnNode) return;

        if (pnjZoneStress != null)
        {
            pnjZoneStress.addStress = false;
            Debug.Log("il t'qimbe bien ce fdp");
        }

        DialogueManager.onNodeDisplayed -= OnNodeDisplayed;
    }
}