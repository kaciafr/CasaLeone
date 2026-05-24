using DG.Tweening;
using PnjWaves;
using Restaurants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiRound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject uiRound;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private RoundEcran roundEcran;
    
    
    [Header("Texte")]
    [SerializeField] private TextMeshProUGUI happyNbr;
    [SerializeField] private TextMeshProUGUI sadNbr;
    [SerializeField] private TextMeshProUGUI scoreNbr;

    private void OnEnable()
    {
        waveSpawner.UiRound += ShowUi;
        waveSpawner.UiOffRound += HideUi;
    }

    private void OnDisable()
    {
        waveSpawner.UiRound -= ShowUi;
        waveSpawner.UiOffRound -= HideUi;
    }


    void Start()
    {
        uiRound.transform.DOScale(Vector3.zero, 0.5f);
    }

    private void ShowUi(WaveSpawner obj)
    {
        uiRound.transform.DOScale(Vector3.one, 0.5f);
        
        happyNbr.text = RoundEcran.Instance.happyScore.ToString();
        sadNbr.text = RoundEcran.Instance.sadScore.ToString();
        scoreNbr.text = RoundEcran.Instance.score.ToString();
    }

    private void HideUi(WaveSpawner obj)
    {
        uiRound.transform.DOScale(Vector3.zero, 0.5f);
    }

    public void OnClick()
    {
        uiRound.transform.DOScale(Vector3.zero, 0.5f);
    }
}
