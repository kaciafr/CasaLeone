using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{
    [Header("Slides")]
    public List<GameObject> Tutos;

    [Header("Navigation")]
    [SerializeField] private string nameScene;
    [SerializeField] private float fadeDuration = 0.4f;

    private int currentIndex = 0;
    private bool isTransitioning = false;
    private List<Image> dots = new List<Image>();


    void Start()
    {
        // Cache toutes les slides
        foreach (GameObject tuto in Tutos)
        {
            CanvasGroup cg = GetOrAddCanvasGroup(tuto);
            cg.alpha = 0f;
            tuto.SetActive(false);
        }
        
        if (Tutos.Count > 0)
            StartCoroutine(FadeIn(Tutos[0]));
    }

    void Update()
    {
        if (isTransitioning) return;

        if (Input.GetKeyDown(KeyCode.RightArrow)
         || Input.GetKeyDown(KeyCode.Return))
            NextTuto();
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
	        SceneManager.LoadScene("CutSceneIntro");
        }
	        

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentIndex > 0)
            PrevTuto();
    }


    public void NextTuto()
    {
        if (isTransitioning) return;

        if (currentIndex >= Tutos.Count - 1)
        {
            StartCoroutine(FadeOutThenLoad());
            return;
        }
        StartCoroutine(CrossFade(currentIndex, currentIndex + 1));
    }

    public void PrevTuto()
    {
        if (isTransitioning || currentIndex <= 0) return;
        StartCoroutine(CrossFade(currentIndex, currentIndex - 1));
    }

    public void GoToSlide(int index)
    {
        if (isTransitioning || index == currentIndex) return;
        if (index < 0 || index >= Tutos.Count) return;
        StartCoroutine(CrossFade(currentIndex, index));
    }


    private IEnumerator CrossFade(int from, int to)
    {
        isTransitioning = true;

        GameObject outGO = Tutos[from];
        GameObject inGO  = Tutos[to];

        inGO.SetActive(true);
        CanvasGroup cgIn  = GetOrAddCanvasGroup(inGO);
        CanvasGroup cgOut = GetOrAddCanvasGroup(outGO);
        cgIn.alpha = 0f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float ratio = Mathf.SmoothStep(0f, 1f, t / fadeDuration);
            cgOut.alpha = 1f - ratio;
            cgIn.alpha  = ratio;
            yield return null;
        }

        cgOut.alpha = 0f;
        cgIn.alpha  = 1f;
        outGO.SetActive(false);

        currentIndex = to;
        isTransitioning = false;
    }

    private IEnumerator FadeIn(GameObject go)
    {
        isTransitioning = true;
        go.SetActive(true);
        CanvasGroup cg = GetOrAddCanvasGroup(go);
        cg.alpha = 0f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.SmoothStep(0f, 1f, t / fadeDuration);
            yield return null;
        }

        cg.alpha = 1f;
        isTransitioning = false;
    }

    private IEnumerator FadeOutThenLoad()
    {
        isTransitioning = true;
        CanvasGroup cg = GetOrAddCanvasGroup(Tutos[currentIndex]);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.SmoothStep(1f, 0f, t / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(nameScene);
    }

    // ─── Helpers ───────────────────────────────────────────────────────────────

    private CanvasGroup GetOrAddCanvasGroup(GameObject go)
    {
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        if (cg == null) cg = go.AddComponent<CanvasGroup>();
        return cg;
    }
}