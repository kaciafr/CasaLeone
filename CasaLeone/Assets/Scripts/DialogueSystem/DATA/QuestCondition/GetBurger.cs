using UnityEngine;

public class GetBurger : MonoBehaviour
{
    private string conditionID = "Burger";
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
 
        ConditionManager.SetCondition("Burger", true);
        Destroy(gameObject);
    }
}
