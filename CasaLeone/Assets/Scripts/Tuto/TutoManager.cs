using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoManager : MonoBehaviour
{

    public List<GameObject> Tutos;
    private int currentindex = 0;
    [SerializeField]private string nameScene;
    private bool isShowPanel; 
    void Start()
    {
        foreach (GameObject tuto in Tutos)
        {
            tuto.SetActive(false);
        }

      
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextTuto();
        }
    }

    private void NextTuto()
    {

        if (currentindex >= Tutos.Count - 1)
        {
            SceneManager.LoadScene(nameScene);
            return;
        }
        
      Tutos[currentindex].SetActive(false);

      currentindex++;
      
      Tutos[currentindex].SetActive(true);
      
    }


    public  void TogglePanel()
    {
        isShowPanel = !isShowPanel;
     
        Tutos[0].SetActive(isShowPanel);
    }

    public void ClosePanel()
    {
        isShowPanel = false;
    }
    
   
}
