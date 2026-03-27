using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/Dialogue Conversation")]
public class DialogueConversation : ScriptableObject
{
    public string conversationID;
    public DialogueNode startingNode;
    public bool canRepeat = true;
}