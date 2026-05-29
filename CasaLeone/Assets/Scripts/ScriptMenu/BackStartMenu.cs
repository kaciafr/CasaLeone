using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackStartMenu : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
