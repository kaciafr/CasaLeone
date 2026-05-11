using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuActions : MonoBehaviour
{
    [Header("Panneaux")]
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject gamePanel;

    [Header("Fade")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.6f;

    private void Start()
    {
        startMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        gamePanel.SetActive(false);

        // Fade in à l'ouverture du menu
        StartCoroutine(Fade(1f, 0f, fadeDuration, null));
    }

    // ── Boutons ──────────────────────────────────────────────────────────────

    public void OnCommencerButton()
    {
        StartCoroutine(Fade(0f, 1f, fadeDuration, () =>
        {
            startMenuPanel.SetActive(false);
            gamePanel.SetActive(true);
            StartCoroutine(Fade(1f, 0f, fadeDuration, null));
        }));
    }

    public void OnOptionsButton()
    {
        optionsPanel.SetActive(true);
    }

    public void OnOptionsCloseButton()
    {
        optionsPanel.SetActive(false);
    }

    // ── Fade ─────────────────────────────────────────────────────────────────

    private IEnumerator Fade(float from, float to, float duration, System.Action onComplete)
    {
        Color c = fadeImage.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeImage.color = new Color(c.r, c.g, c.b, Mathf.Lerp(from, to, elapsed / duration));
            yield return null;
        }

        fadeImage.color = new Color(c.r, c.g, c.b, to);
        onComplete?.Invoke();
    }
}