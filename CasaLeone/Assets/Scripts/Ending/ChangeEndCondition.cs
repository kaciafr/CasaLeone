using DialogueSystem.DATA;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeEndCondition : MonoBehaviour
{
    [SerializeField] private EndingGame        endingGame;
    [SerializeField] private ConditionDatabase conditionDatabase;

    void OnEnable()
    {
        ConditionManager.onConditionChanged += OnConditionChanged;
    }

    void OnDisable()
    {
        ConditionManager.onConditionChanged -= OnConditionChanged;
    }

    void OnConditionChanged(string id, bool value)
    {
        if (!value) return; 

        if (!AllConditionsCompleted()) return;

        endingGame.endScript.changedEnd = true;
        endingGame.endScript.burnOutEnd = false;
        endingGame.endScript.fireEnd = false;
        endingGame.endScript.perroquetEnd = false;

        SceneManager.LoadScene("EndScene");
    }

    bool AllConditionsCompleted()
    {
        if (conditionDatabase == null || conditionDatabase.conditions.Count == 0)
            return false;

        foreach (var condition in conditionDatabase.conditions)
            if (!ConditionManager.CheckCondition(condition.conditionID))
                return false;

        return true;
    }
}