using System;
using DialogueSystem; // Accès à NPCTriggerDialogue
using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using Players;
using Players.Interaction;
using UnityEngine;

public class YellowBird : MonoBehaviour, IInteractable
{
	private GlobalPlayer currentPlayer;
	[SerializeField] private GameObject yellowBirds;
	[SerializeField] private DialogueConversation dialogueOiseau;
    
	private NPCTriggerDialogue npcDialogue;

	public event Action<YellowBird> OnYellowBird;

	private void Awake()
	{
		npcDialogue = GetComponent<NPCTriggerDialogue>();
	}

	public void Interact(GlobalPlayer globalPlayer)
	{
		if (DialogueManager.Instance.IsInConversation)
		{
			DialogueManager.Instance.PlayerAdvance();
			return;
		}

		if (dialogueOiseau != null && npcDialogue != null)
		{
			npcDialogue.TriggerDialogue(dialogueOiseau, () => { FinalizeBird(globalPlayer); });
		}
	}

	private void FinalizeBird(GlobalPlayer globalPlayer)
	{
		if (yellowBirds != null) Destroy(yellowBirds);
        
		globalPlayer.yellowBird.SetActive(true);
		OnYellowBird?.Invoke(this);
        
		Destroy(gameObject);
	}
}