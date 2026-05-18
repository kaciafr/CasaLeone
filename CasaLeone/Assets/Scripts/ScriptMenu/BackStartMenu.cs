using UnityEngine;
using UnityEngine.SceneManagement;

public class BackStartMenu : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene("FinalScene");
    }
}
