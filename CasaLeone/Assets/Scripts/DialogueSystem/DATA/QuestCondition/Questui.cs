using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [Header("Références")]
    public GameObject  questPanel;
    public Transform   questListContainer;
    public GameObject  questEntryPrefab;
    private bool active = true;
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
	    active = !active;
	    if (active==false) questPanel.SetActive(true);
        else ClosePanel();
        RefreshUI(null);
    }

    public void ClosePanel()
    {
	    active = true;
        questPanel.SetActive(false);
    }

    void RefreshUI(DialogueSystem.DATA.DialogueCondition _)
    {
        foreach (Transform child in questListContainer)
            Destroy(child.gameObject);

        if (QuestManager.Instance == null) return;
        
        foreach (var quest in QuestManager.Instance.activeQuests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContainer);
            QuestEntryUI entryUI = entry.GetComponent<QuestEntryUI>();
            if (entryUI != null)
                entryUI.Setup(quest.condition.questName, quest.condition.description, quest.isCompleted);
        }
    }
}