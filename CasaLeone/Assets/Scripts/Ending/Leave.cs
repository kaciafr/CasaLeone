using Players;
using Players.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leave : MonoBehaviour,IInteractable
{
	public int Priotity => 1;
	[SerializeField] private EndingGame endingGame;
	public void Interact(GlobalPlayer globalPlayer)
	{
		endingGame.endScript.fireEnd =  true;
		SceneManager.LoadScene("EndScene");
	}

}
