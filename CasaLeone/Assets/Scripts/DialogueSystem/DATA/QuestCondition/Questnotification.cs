using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// Notification en haut de l'écran quand une quête est ajoutée.
/// Apparaît, reste quelques secondes, puis disparaît.
/// 
/// Hiérarchie UI suggérée (sur un Canvas Screen Space Overlay) :
///   QuestNotification  ← ce script + CanvasGroup
///   └── Background (Image)
///       └── Message (TextMeshProUGUI)
/// </summary>
public class QuestNotification : MonoBehaviour
{
    [Header("Références")]
    public TextMeshProUGUI messageText;
    public CanvasGroup     canvasGroup;

    [Header("Settings")]
    public float displayDuration = 3f;   // durée d'affichage en secondes
    public float fadeDuration    = 0.4f; // durée du fade in/out
    public string messageTemplate = "Nouvelle mission ajoutée au carnet !";

    private Coroutine _notifCoroutine;

    void Awake()
    {
        if (canvasGroup != null)
            canvasGroup.alpha = 0f;
    }

    void OnEnable()
    {
        QuestManager.onQuestAdded += ShowNotification;
    }

    void OnDisable()
    {
        QuestManager.onQuestAdded -= ShowNotification;
    }

    void ShowNotification(DialogueSystem.DATA.DialogueCondition condition)
    {
        if (messageText != null)
            messageText.text = messageTemplate;

        if (_notifCoroutine != null)
            StopCoroutine(_notifCoroutine);

        _notifCoroutine = StartCoroutine(NotificationRoutine());
    }

    IEnumerator NotificationRoutine()
    {
        // Fade in
        canvasGroup.DOFade(1f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        // Reste affiché
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        canvasGroup.DOFade(0f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        _notifCoroutine = null;
    }
}