using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadScene
{
    public class LoadScene : MonoBehaviour
    {
        public static LoadScene Instance;

        private void EndResignation()
        {
            SceneManager.LoadScene("EndResignation");
        }

        private void EndParrot()
        {
            SceneManager.LoadScene("EndParrot");
        }


        private void EndChanged()
        {
            SceneManager.LoadScene("EndChanged");
        }
        
        private void EndBurnout()
        {
            SceneManager.LoadScene("EndBurnout");
        }
        
        private void PlayScene()
        {
            SceneManager.LoadScene("PlayScene");
        }
        
        private void StartScene()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}