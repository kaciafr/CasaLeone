using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadScene
{
    public class LoadScene : MonoBehaviour
    {
        public static LoadScene Instance;
        
        public void StartScene()
        {
            SceneManager.LoadScene("StartMenu");
        }

        public void TutoScene()
        {
            SceneManager.LoadScene("TutoScene");
        }

        public void FinalScene()
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}