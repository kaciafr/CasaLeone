using DG.Tweening;
using Players;
using Players.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leave : MonoBehaviour
{
	[SerializeField] private EndingGame endingGame;
	[SerializeField] private GameObject leavingWayUi;
	public void Interact()
	{
		endingGame.endScript.fireEnd =  true;
		SceneManager.LoadScene("EndScene");
	}
	
	public void Exit()
	{
		Time.timeScale = 1;
		leavingWayUi.SetActive(false);
	}

}
