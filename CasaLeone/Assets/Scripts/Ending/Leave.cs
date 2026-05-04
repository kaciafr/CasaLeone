using Players;
using Players.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leave : MonoBehaviour,IInteractable
{
	[SerializeField] private EndingGame endingGame;
	public void Interact(GlobalPlayer globalPlayer)
	{
		endingGame.endScript.fireEnd =  true;
		SceneManager.LoadScene("EndScene");
	}
}
