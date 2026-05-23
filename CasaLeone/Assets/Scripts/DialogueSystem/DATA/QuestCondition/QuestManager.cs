using System;
using System.Collections.Generic;
using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Références")]
    public ConditionDatabase conditionDatabase;

    [System.Serializable]
    public class QuestEntry
    {
        public DialogueCondition condition;
        public bool              isCompleted;
    }

    public List<QuestEntry> activeQuests = new List<QuestEntry>();

    public static event Action<DialogueCondition> onQuestAdded;
    public static event Action<DialogueCondition> onQuestCompleted;

    [System.Serializable]
    public class NodeQuestLink
    {
        [Tooltip("La node qui déclenche l'ajout de la quête")]
        public DialogueNode node;
        [Tooltip("L'ID de la condition associée à cette quête")]
        public string       conditionID;
    }

    [Header("Liens Node → Quête")]
    public NodeQuestLink[] nodeQuestLinks;

    void Awake()
    {
        if (Instance == null) Instance = this;
        activeQuests.Clear();
    }

    void OnEnable()
    {
        DialogueManager.onNodeDisplayed     += OnNodeDisplayed;
        ConditionManager.onConditionChanged += OnConditionChanged;
    }

    void OnDisable()
    {
        DialogueManager.onNodeDisplayed     -= OnNodeDisplayed;
        ConditionManager.onConditionChanged -= OnConditionChanged;
    }

    void OnConditionChanged(string conditionID, bool value)
    {
        if (!value) return;

        foreach (var quest in activeQuests)
        {
            if (quest.isCompleted) continue;
            if (quest.condition.conditionID != conditionID) continue;

            quest.isCompleted = true;
            onQuestCompleted?.Invoke(quest.condition);
        }
    }

    void OnNodeDisplayed(DialogueNode node)
    {
        foreach (var link in nodeQuestLinks)
        {
            if (link.node != node) continue;

            DialogueCondition condition = FindCondition(link.conditionID);
            if (condition == null) continue;

            if (IsQuestActive(link.conditionID)) continue;

            activeQuests.Add(new QuestEntry { condition = condition, isCompleted = false });
            onQuestAdded?.Invoke(condition);
        }
    }

    DialogueCondition FindCondition(string conditionID)
    {
        foreach (var condition in conditionDatabase.conditions)
            if (condition.conditionID == conditionID)
                return condition;
        return null;
    }

    bool IsQuestActive(string conditionID)
    {
        foreach (var quest in activeQuests)
            if (quest.condition.conditionID == conditionID)
                return true;
        return false;
    }
}