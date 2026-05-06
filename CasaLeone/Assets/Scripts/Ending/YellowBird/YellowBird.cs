using System;
using DialogueSystem.Runtime;
using Players;
using Players.Interaction;
using PnjWaves;
using UnityEngine;

public class YellowBird : MonoBehaviour , IInteractable
{
    private GlobalPlayer currentPlayer;
    [SerializeField] private GameObject yellowBirds;
    [SerializeField] private GameObject uiYellowBird;
    [SerializeField] private GameObject bubblePrefab;
    
    public event Action steelYellowBird;

    private void Start()
    {
        uiYellowBird.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        currentPlayer = other.gameObject.GetComponent<GlobalPlayer>();
    }

    private void OnTriggerExit(Collider other)
    {
        currentPlayer = null;
    }

    public void Interact(GlobalPlayer globalPlayer)
    {
        uiYellowBird.SetActive(true);
        Destroy(yellowBirds);
        globalPlayer.yellowBird.SetActive(true);
        steelYellowBird?.Invoke();
    }
}
