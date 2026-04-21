using DialogueSystem.DATA;
using UnityEngine;

public class GetObjectForCondition : MonoBehaviour
{
    public string conditionID = "";
 
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
 
        ConditionManager.SetCondition(conditionID, true);
        Destroy(gameObject);
    }
}
