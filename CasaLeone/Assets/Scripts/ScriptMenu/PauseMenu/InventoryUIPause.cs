using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIPause : MonoBehaviour
{
    private bool IsPaused = false;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject Button;
    [SerializeField] private RectTransform PauseMenuRect; 

    private void Start()
    {
        PauseMenu.SetActive(false);
        Button.SetActive(false);
    }

    public void Pause()
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        {
            PauseMenu.SetActive(true);
            Button.SetActive(true);
            Time.timeScale = 0;

            PauseMenuRect.DOKill();
          
        }
        else
        {
            Button.SetActive(false);
            PauseMenu.SetActive(false);


            PauseMenuRect.DOKill();
        }
    }
}