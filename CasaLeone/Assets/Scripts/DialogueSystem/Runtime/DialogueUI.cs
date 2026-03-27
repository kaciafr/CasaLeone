using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject dialoguePanel;
    public Image portraitImage;
    public TextMeshProUGUI speakerText;

    [Header("Sprite Sequence")]
    public Transform spriteSequenceContainer;

    [Header("Choices")]
    public Transform choicesContainer;
    public GameObject choiceButtonPrefab;

    private float revealInterval = 0.18f;

    private Coroutine revealCoroutine;
    private List<GameObject> spawnedSprites = new List<GameObject>();

    private string currentConditionID = "";


    public void ShowUI() => dialoguePanel.SetActive(true);

    public void HideUI()
    {
        dialoguePanel.SetActive(false);
        StopReveal();
        ClearSequence();
    }

    public void Display(DialogueNode node)
    {
        Display(node, "");
    }

    public void Display(DialogueNode node, string conditionID)
    {
        currentConditionID = conditionID;

        if (portraitImage != null) portraitImage.sprite = node.portrait;
        if (speakerText   != null) speakerText.text     = node.speakerName;

        ClearSequence();
        ClearChoices();

        if (revealCoroutine != null) StopCoroutine(revealCoroutine);
        revealCoroutine = StartCoroutine(RevealSprites(node.spritePrefabs, () => BuildChoices(node)));
    }


    IEnumerator RevealSprites(List<GameObject> prefabs, System.Action onComplete)
    {
        if (prefabs != null)
        {
            foreach (GameObject prefab in prefabs)
            {
                if (prefab == null) continue;
                GameObject instance = Instantiate(prefab, spriteSequenceContainer);
                spawnedSprites.Add(instance);
                StartCoroutine(PopIn(instance.transform));
                yield return new WaitForSeconds(revealInterval);
            }
        }
        onComplete?.Invoke();
    }

    IEnumerator PopIn(Transform t)
    {
        t.localScale = Vector3.zero;
        float elapsed = 0f;
        while (elapsed < 0.12f)
        {
            elapsed += Time.deltaTime;
            t.localScale = Vector3.one * Mathf.SmoothStep(0f, 1f, elapsed / 0.12f);
            yield return null;
        }
        t.localScale = Vector3.one;
    }

    void StopReveal()
    {
        if (revealCoroutine != null) { StopCoroutine(revealCoroutine); revealCoroutine = null; }
    }

    void ClearSequence()
    {
        foreach (var go in spawnedSprites) if (go != null) Destroy(go);
        spawnedSprites.Clear();
        foreach (Transform child in spriteSequenceContainer) Destroy(child.gameObject);
    }


    void BuildChoices(DialogueNode node)
    {
        ClearChoices();

        if (node.choices != null && node.choices.Count > 0)
        {
            foreach (var choice in node.choices)
            {
                if (choice.requiresCondition && !ConditionManager.CheckCondition(choice.conditionID))
                    continue;

                GameObject buttonObj = Instantiate(choiceButtonPrefab, choicesContainer);
                FillButtonSprites(buttonObj, choice.choiceSpritePrefabs);

                DialogueChoice captured = choice;
                buttonObj.GetComponent<Button>().onClick.AddListener(() =>
                    DialogueManager.Instance.SelectChoice(captured));
            }
        }
        else
        {
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choicesContainer);
            Button btn = buttonObj.GetComponent<Button>();
            TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            bool hasCondition = currentConditionID != "";

            if (hasCondition)
            {
                StartCoroutine(WaitForConditionThenUnlock(btn, label));
            }
            else
            {
                if (label != null) label.text = "Fin";
                btn.onClick.AddListener(() => DialogueManager.Instance.Next());
            }
        }
    }
    
    IEnumerator WaitForConditionThenUnlock(Button btn, TextMeshProUGUI label)
    {
        while (!ConditionManager.CheckCondition(currentConditionID))
            yield return null;  

        btn.interactable = true;
        if (label != null) label.text = "→";
        btn.onClick.AddListener(() => DialogueManager.Instance.Next());
    }

    void FillButtonSprites(GameObject buttonObj, List<GameObject> prefabs)
    {
        Transform container = buttonObj.transform.Find("SpriteContainer");
        if (container != null && prefabs != null)
            foreach (var p in prefabs)
                if (p != null) Instantiate(p, container);

        if (prefabs == null || prefabs.Count == 0)
        {
            TextMeshProUGUI tmp = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null) tmp.text = "→";
        }
    }

    void ClearChoices()
    {
        foreach (Transform child in choicesContainer) Destroy(child.gameObject);
    }
}