using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ending
{
	public class CutScene : MonoBehaviour
	{
		public float TimeCutScene;
		public string sceneName;
		void Update()
		{
			TimeCutScene -= Time.deltaTime;

			if (TimeCutScene <= 0)
			{
				SceneManager.LoadScene(sceneName);
			}
        
		}

		public void Skip()
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}
