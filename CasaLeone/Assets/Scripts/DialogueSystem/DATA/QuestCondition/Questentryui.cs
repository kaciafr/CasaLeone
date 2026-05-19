using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Une entrée dans le carnet de quêtes.
/// 
/// Hiérarchie UI suggérée :
///   QuestEntry
///   ├── QuestName        (TextMeshProUGUI)
///   ├── QuestDescription (TextMeshProUGUI)
///   └── CompletedIcon    (GameObject — une coche verte)
/// </summary>
public class QuestEntryUI : MonoBehaviour
{
    [Header("Références")]
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questDescriptionText;
    public GameObject      completedIcon; // coche ou autre visuel de validation

    public void Setup(string questName, string description, bool isCompleted)
    {
        if (questNameText        != null) questNameText.text        = questName;
        if (questDescriptionText != null) questDescriptionText.text = description;

        if (completedIcon != null)
            completedIcon.SetActive(isCompleted);

        // Texte barré si complété
        if (questNameText != null)
            questNameText.fontStyle = isCompleted
                ? FontStyles.Strikethrough
                : FontStyles.Normal;
    }
}