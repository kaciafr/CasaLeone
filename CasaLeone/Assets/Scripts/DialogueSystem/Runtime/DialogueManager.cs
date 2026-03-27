using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private DialogueNode currentNode;
    private DialogueConversation currentConversation;
    private Action onEndedCallback;

    private string requiredConditionID = "";

    public DialogueUI dialogueUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartConversation(DialogueConversation conversation)
    {
        StartConversation(conversation, "", null);
    }

    public void StartConversation(DialogueConversation conversation, string conditionID, Action onEnded)
    {
        currentConversation = conversation;
        currentNode = conversation.startingNode;
        onEndedCallback = onEnded;
        requiredConditionID = conditionID;

        dialogueUI.ShowUI();
        DisplayNode(currentNode);
    }

    void DisplayNode(DialogueNode node)
    {
        dialogueUI.Display(node, requiredConditionID);
    }

    public void SelectChoice(DialogueChoice choice)
    {
        if (choice.nextNode != null)
        {
            currentNode = choice.nextNode;
            DisplayNode(currentNode);
        }
        else
        {
            EndConversation();
        }
    }

    public void Next()
    {
        if (currentNode.nextNode != null)
        {
            currentNode = currentNode.nextNode;
            DisplayNode(currentNode);
        }
        else
        {
            EndConversation();
        }
    }

    void EndConversation()
    {
        if (currentConversation != null && !currentConversation.canRepeat)
            ConditionManager.SetCondition(currentConversation.conversationID + "_done", true);

        dialogueUI.HideUI();

        Action callback = onEndedCallback;
        currentConversation = null;
        currentNode = null;
        onEndedCallback = null;
        requiredConditionID = "";

        callback?.Invoke();
    }
}