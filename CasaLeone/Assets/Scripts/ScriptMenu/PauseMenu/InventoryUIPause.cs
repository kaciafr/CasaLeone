using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIPause : MonoBehaviour
{
    private bool IsPaused = false;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject PanelButton;
    [SerializeField] private RectTransform PauseMenuRect;
    [SerializeField] private InvRead InvRead;


    private void Start()
    {
        PauseMenu.SetActive(false);
        PanelButton.SetActive(false);
    }

    public void Pause()
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        {
            PauseMenu.SetActive(true);
            PanelButton.SetActive(true);
            Time.timeScale = 0;

            PauseMenuRect.DOKill();
            
          
        }
        else
        {
            PanelButton.SetActive(false);
            PauseMenu.SetActive(false);
            PauseMenuRect.DOKill();
            InvRead.Instance.SlideButtonsIn();
        }
    }
}