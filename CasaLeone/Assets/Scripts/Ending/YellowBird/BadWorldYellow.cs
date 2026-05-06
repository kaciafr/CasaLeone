using DialogueSystem.DATA;
using DialogueSystem.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadWorldYellow : MonoBehaviour
{
	[Header("Besoin de : ")]
	[SerializeField] private GameObject uiYellowBird;
	[SerializeField] private YellowBird haveTheBird;
	[SerializeField] private EndingGame endingGame;
	
	[Header("Dialogue")]
	
	public GameObject firstDialogue;
	public GameObject secondDialogue;
	public GameObject thirdDialogue;

	private int sequence = 0;
	
	private void OnEnable()
	{
		haveTheBird.OnYellowBird += PushToTalk;
	}

	private void OnDisable()
	{
		haveTheBird.OnYellowBird -= PushToTalk;
	}
	
	private void Start()
	{
		uiYellowBird.SetActive(false);
		
		firstDialogue.SetActive(false);
		secondDialogue.SetActive(false);
		thirdDialogue.SetActive(false);
	}
	public void PushToTalk(YellowBird bird)
	{
		uiYellowBird.SetActive(true);
		
		switch (sequence)
		{
			case 0:
				sequence++;
				break;
			case 1 :
				firstDialogue.SetActive(true);
				Destroy(firstDialogue,4);
				sequence++;
				break;
			case 2 :
				secondDialogue.SetActive(true);
				Destroy(secondDialogue,4);
				sequence++;
				break;
			case 3 :
				thirdDialogue.SetActive(true);
				Destroy(thirdDialogue,4);
				sequence++;
				break;
			case 4 :
				GameOver();
				break;
		}
	}

	private void GameOver()
	{
		endingGame.endScript.perroquetEnd =  true;
		SceneManager.LoadScene("EndScene");
	}

	
}
