using UnityEngine;

public class InventoryUIPause : MonoBehaviour
{
   private bool IsPaused = false;
   [SerializeField] private GameObject PauseMenu;

   private void Start()
   {
      PauseMenu.SetActive(false);
   }
   public void Pause()
   {
      IsPaused = !IsPaused;
      if (IsPaused)
      {
         PauseMenu.SetActive(true);
         Time.timeScale = 0;
      }
      else
      {
         Time.timeScale = 1;
         PauseMenu.SetActive(false);
      }
   }
}
