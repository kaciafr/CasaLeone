using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UiToilette : MonoBehaviour
{
    [SerializeField] private LostStressToilet toilette;
    [SerializeField] private List<Light> lights;
    [SerializeField] private Transform pivotPorte;

    private void Start()
    {
        toilette = GetComponent<LostStressToilet>();
    }
    private void OnEnable()
    {
        toilette.StressIsFull += StreessIsFull;
        toilette.StressIsEmpty += StreessIsEmpty;
        toilette.Exit += CloseDoor;
    }

    private void OnDisable()
    {
        toilette.StressIsFull -= StreessIsFull;
        toilette.StressIsEmpty -= StreessIsEmpty;
        toilette.Exit -= CloseDoor;
    }

    private void CloseDoor()
    {
        pivotPorte.transform.DOLocalRotate(new Vector3(0,90,0),1f).SetEase(Ease.OutBack);
    }

    private void StreessIsFull()
    {
        foreach (Light l in lights)
        {
            l.color = Color.wheat;
            pivotPorte.transform.DOLocalRotate(new Vector3(0,190,0),1f).SetEase(Ease.OutBack);
        }
    }
    private void StreessIsEmpty()
    {
        foreach (Light l in lights)
        {
            l.color = Color.red;
        }
    }
}
