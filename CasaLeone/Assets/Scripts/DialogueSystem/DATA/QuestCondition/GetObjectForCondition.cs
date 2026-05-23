using DialogueSystem.DATA;
using UnityEngine;

public class GetObjectForCondition : MonoBehaviour
{
    [Tooltip("La DialogueConversation du NPC à qui rendre l'objet")]
    public DialogueConversation linkedConversation;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (linkedConversation != null)
            linkedConversation.itemCollected = true;

        Destroy(gameObject);
    }
}