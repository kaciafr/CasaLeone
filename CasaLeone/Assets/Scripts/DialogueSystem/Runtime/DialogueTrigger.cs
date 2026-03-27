using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueConversation conversation;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartConversation(conversation);
    }
}