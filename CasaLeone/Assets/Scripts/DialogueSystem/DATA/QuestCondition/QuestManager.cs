using System;
using System.Collections.Generic;
using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;

/// <summary>
/// Gère le carnet de quêtes.
/// Écoute onNodeDisplayed pour ajouter une quête quand la bonne node se joue.
/// Vérifie chaque frame si les quêtes en cours sont validées.
/// </summary>
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

    // Liste des quêtes actives (en cours + terminées)
    public List<QuestEntry> activeQuests = new List<QuestEntry>();

    // Event déclenché quand une quête est ajoutée
    public static event Action<DialogueCondition> onQuestAdded;
    // Event déclenché quand une quête est complétée
    public static event Action<DialogueCondition> onQuestCompleted;

    // Associe une node à une conditionID pour savoir quelle quête ajouter
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
    }

    void OnEnable()
    {
        DialogueManager.onNodeDisplayed += OnNodeDisplayed;
    }

    void OnDisable()
    {
        DialogueManager.onNodeDisplayed -= OnNodeDisplayed;
    }

    void Update()
    {
        // Vérifie si des quêtes en cours viennent d'être complétées
        foreach (var quest in activeQuests)
        {
            if (quest.isCompleted) continue;
            if (!ConditionManager.CheckCondition(quest.condition.conditionID)) continue;

            quest.isCompleted = true;
            onQuestCompleted?.Invoke(quest.condition);
        }
    }

    void OnNodeDisplayed(DialogueNode node)
    {
        foreach (var link in nodeQuestLinks)
        {
            if (link.node != node) continue;

            // Cherche la condition dans la database
            DialogueCondition condition = FindCondition(link.conditionID);
            if (condition == null) continue;

            // Vérifie qu'elle n'est pas déjà dans le carnet
            if (IsQuestActive(link.conditionID)) continue;

            // Ajoute la quête
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