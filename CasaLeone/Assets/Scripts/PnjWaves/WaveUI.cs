using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [Header("Référence")]
    public WaveSpawner spawner;

    [Header("Textes")]
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