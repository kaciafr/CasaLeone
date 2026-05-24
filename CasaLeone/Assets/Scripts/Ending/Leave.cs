using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Leave : MonoBehaviour
{
	[SerializeField] private EndingGame endingGame;
	[SerializeField] private GameObject leavingWayUi;
	[SerializeField] private Image endGameSmooth;
	public void Interact()
	{
		endingGame.endScript.fireEnd =  true;
		Time.timeScale = 1;
		endGameSmooth.DOFade(1, 3f).SetEase(Ease.Linear).onComplete += () =>
		{
			SceneManager.LoadScene("EndScene");
		};
	}
	
	public void Exit()
	{
		Time.timeScale = 1;
		leavingWayUi.SetActive(false);
	}

}
