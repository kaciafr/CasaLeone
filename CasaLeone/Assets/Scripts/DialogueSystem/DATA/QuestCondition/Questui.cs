using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel du carnet de quêtes dans le menu pause.
/// Se rafraîchit quand une quête est ajoutée ou complétée.
/// 
/// Hiérarchie UI suggérée :
///   QuestPanel
///   └── ScrollView
///       └── Content          ← QuestEntryPrefab instanciés ici
/// </summary>
public class QuestUI : MonoBehaviour
{
    [Header("Références")]
    public GameObject  questPanel;
    public Transform   questListContainer; // le Content du ScrollView
    public GameObject  questEntryPrefab;   // prefab d'une entrée de quête

    void OnEnable()
    {
        QuestManager.onQuestAdded     += RefreshUI;
        QuestManager.onQuestCompleted += RefreshUI;
    }

    void OnDisable()
    {
        QuestManager.onQuestAdded     -= RefreshUI;
        QuestManager.onQuestCompleted -= RefreshUI;
    }

    public void OpenPanel()
    {
        questPanel.SetActive(true);
        RefreshUI(null);
    }

    public void ClosePanel()
    {
        questPanel.SetActive(false);
    }

    void RefreshUI(DialogueSystem.DATA.DialogueCondition _)
    {
        // Vide la liste
        foreach (Transform child in questListContainer)
            Destroy(child.gameObject);

        if (QuestManager.Instance == null) return;

        // Recrée une entrée par quête
        foreach (var quest in QuestManager.Instance.activeQuests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContainer);
            QuestEntryUI entryUI = entry.GetComponent<QuestEntryUI>();
            if (entryUI != null)
                entryUI.Setup(quest.condition.questName, quest.condition.description, quest.isCompleted);
        }
    }
}