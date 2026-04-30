using Ending;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private GameObject fireEnd;
    [SerializeField] private GameObject changedEnd;
    [SerializeField] private GameObject burnOutEnd;
    [SerializeField] private GameObject perroquetteEnd;
    
    [SerializeField] private EndScript endScript;

    private void Update()
    {
        fireEnd.SetActive(endScript.fireEnd);
        changedEnd.SetActive(endScript.changedEnd);
        burnOutEnd.SetActive(endScript.burnOutEnd);
        perroquetteEnd.SetActive(endScript.perroquetEnd);
    }
}
