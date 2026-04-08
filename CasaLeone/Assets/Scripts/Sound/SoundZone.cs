using UnityEngine;

namespace Sound
{
    public class MusicZone : MonoBehaviour
    {
        [SerializeField] private MusicType musicType; 
        
        [SerializeField] private float fadeDuration = 1.5f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SoundManager.Instance.PlayMusic(musicType);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SoundManager.Instance.StopMusic(0.5f);  
            }
        }


    }
}