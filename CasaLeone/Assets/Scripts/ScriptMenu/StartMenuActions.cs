using System.Collections;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuActions : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.6f;
    private bool active = true;

    [Header("Panels")]
    [SerializeField] private RectTransform mainPanel;      
    [SerializeField] private RectTransform secondPanel;  

    [Header("Animation - Main Panel")]
    [SerializeField] private float bounceDownAmount = 30f; 
    [SerializeField] private float bounceDuration   = 0.12f; 
    [SerializeField] private float exitDuration     = 0.45f;

    [Header("Animation - Second Panel")]
    [SerializeField] private float secondPanelStartOffsetY = -200f;
    [SerializeField] private float secondPanelRiseDuration = 0.7f;  
    [SerializeField] private float secondPanelDelay        = 0.1f;  
    [SerializeField] private float secondPanelExtraRiseY   = 80f; 


    private Vector2 mainPanelOrigin;
    private Vector2 secondPanelOrigin;

    private void Start()
    {
        mainPanelOrigin   = mainPanel.anchoredPosition;
        secondPanelOrigin = secondPanel.anchoredPosition;

        secondPanel.anchoredPosition = secondPanelOrigin + new Vector2(0f, secondPanelStartOffsetY);

        StartCoroutine(Fade(1f, 0f, fadeDuration, null));
    }

    public void OnStartButton()
    {
        StartCoroutine(PlayStartSequence());
    }
    public void OnOtherButton()
    {
        StartCoroutine(PlayAutreSequence());
    }

    // ─── Séquence principale ───────────────────────────────────────────────────

    private IEnumerator PlayStartSequence()
    {
	    
        StartCoroutine(MainPanelExit());
        StartCoroutine(SecondPanelRise());

        // Attend que tout soit fini puis fade → LoadScene
        float totalDuration = bounceDuration + exitDuration + 0.05f;
        yield return new WaitForSeconds(totalDuration);

        StartCoroutine(Fade(0f, 1f, fadeDuration, () =>
        {
            SceneManager.LoadScene("TutoScene");
        }));
    }
    
    private IEnumerator PlayAutreSequence()
    {
	    
        StartCoroutine(MainPanelExit());
        StartCoroutine(SecondPanelRise());

        // Attend que tout soit fini puis fade → LoadScene
        float totalDuration = bounceDuration + exitDuration + 0.05f;
        yield return new WaitForSeconds(totalDuration);

        StartCoroutine(Fade(0f, 1f, fadeDuration, () =>
        {
            SceneManager.LoadScene("CreditScene");
        }));
    }

    // ─── Main panel : rebond vers le bas puis envol vers le haut ──────────────

    private IEnumerator MainPanelExit()
    {
        // Phase 1 : petit recul vers le bas (effet "élan")
        Vector2 bounceTarget = mainPanelOrigin + new Vector2(0f, -bounceDownAmount);
        yield return MovePanel(mainPanel, mainPanelOrigin, bounceTarget, bounceDuration, Ease.OutQuad);

        // Phase 2 : envol vers le haut, hors écran
        float screenHeight  = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height;
        Vector2 exitTarget  = mainPanelOrigin + new Vector2(0f, screenHeight + mainPanel.rect.height);
        yield return MovePanel(mainPanel, bounceTarget, exitTarget, exitDuration, Ease.InBack);
    }

    // ─── Second panel : monte doucement vers sa position d'origine ────────────

    private IEnumerator SecondPanelRise()
    {
        yield return new WaitForSeconds(secondPanelDelay);

        Vector2 from   = secondPanelOrigin + new Vector2(0f, secondPanelStartOffsetY);
        Vector2 target = secondPanelOrigin + new Vector2(0f, secondPanelExtraRiseY); // ← ici
        yield return MovePanel(secondPanel, from, target, secondPanelRiseDuration, Ease.OutCubic);
    }

    // ─── Utilitaire de déplacement avec easing ────────────────────────────────

    private IEnumerator MovePanel(RectTransform rt, Vector2 from, Vector2 to, float duration, Ease ease)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rt.anchoredPosition = Vector2.LerpUnclamped(from, to, ApplyEase(t, ease));
            yield return null;
        }

        rt.anchoredPosition = to;
    }

    // ─── Fonctions d'easing ───────────────────────────────────────────────────

    private enum Ease { OutQuad, InBack, OutCubic }

    private float ApplyEase(float t, Ease ease)
    {
        switch (ease)
        {
            case Ease.OutQuad:
                return 1f - (1f - t) * (1f - t);

            case Ease.InBack:
                // Accélère avec un léger dépassement au début
                float s = 1.70158f;
                return t * t * ((s + 1f) * t - s);

            case Ease.OutCubic:
                return 1f - Mathf.Pow(1f - t, 3f);

            default:
                return t;
        }
    }

    // ─── Fade ─────────────────────────────────────────────────────────────────

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