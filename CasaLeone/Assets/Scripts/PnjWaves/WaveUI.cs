using TMPro;
using UnityEngine;

namespace PnjWaves
{
    public class WaveUI : MonoBehaviour
    {
        public WaveSpawner spawner;
        public TextMeshProUGUI currentWaveText;
        public TextMeshProUGUI bestWaveText;

        void Update()
        {
            if (currentWaveText != null)
                currentWaveText.text = $"Vague {spawner.CurrentWave}";

            if (bestWaveText != null)
                bestWaveText.text = $"Meilleur : {spawner.BestWave}";
        }
    }
}