using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuActions : MonoBehaviour
{
    [Header("Panneaux")]
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject gamePanel;

    [Header("Fade")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.6f;
    private bool active = true;

    private void Start()
    {
        optionsPanel.SetActive(true);
        StartCoroutine(Fade(1f, 0f, fadeDuration, null));
    }

    public void OnOptionsButton()
    {
	    active = !active;
	    Debug.Log(active);
	    if (active == false)
	    {
			optionsPanel.SetActive(true);
		    
	    }
	    else
		    OnOptionsCloseButton();
    }

    public void OnOptionsCloseButton()
    {
        optionsPanel.SetActive(false);
        active = true;
    }


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