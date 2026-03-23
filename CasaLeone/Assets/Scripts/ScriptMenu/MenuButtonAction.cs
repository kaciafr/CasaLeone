using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuButtonAction : MonoBehaviour
{
    public GameObject optionContainerGO;
    private float speed = 5f;
    bool isOptionOpen = false;
    public string sceneName;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private Vector3 closedPos;
    private Vector3 openPos;

    void Start()
    {
        closedPos = optionContainerGO.transform.localPosition;
        openPos = closedPos + new Vector3(-300, 0, 0); 
    }

    void Update()
    {
        Vector3 target = isOptionOpen ? openPos : closedPos;
        optionContainerGO.transform.localPosition = Vector3.Lerp(
            optionContainerGO.transform.localPosition,
            target,
            Time.deltaTime * speed
        );
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OpenOptions();
        }
    }

    public void OpenOptions()
    {
        isOptionOpen = !isOptionOpen;
    }
    
    public void Quit()
    {
        Application.Quit();
    }

}