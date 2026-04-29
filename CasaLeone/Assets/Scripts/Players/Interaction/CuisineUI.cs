using System;
using Restaurants;
using UnityEngine;

public class CuisineUI : MonoBehaviour
{
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem bigFire;
    [SerializeField] private ParticleSystem madMAxFire;
    private void Start()
    {
        Restaurant.Instance.OnStressStateChanged += UpdateFlamme;
    }

    private void OnDestroy()
    {
        Restaurant.Instance.OnStressStateChanged -= UpdateFlamme;
    }
    
    private void UpdateFlamme(IStressBar stressBar)
    {
        Debug.Log($"StressBar type: {stressBar?.GetType().Name ?? "NULL"}");
        StopAll();
        switch (stressBar)
        {
            case NormalState:
                fire.Stop();
                bigFire.Stop();
                madMAxFire.Stop();
                break;
                
            case LightStressState :
                fire.Play();
                bigFire.Stop();
                madMAxFire.Stop();
                break;
            case HightStressState :
                fire.Play();
                bigFire.Play();
                break;
            case MadMaxSStressState :
                fire.Play();
                bigFire.Play();
                madMAxFire.Play();
                break;
        }
    }

    private void StopAll()
    {
        fire.Stop();
        fire.Clear();
        bigFire.Stop();
        bigFire.Clear();
        madMAxFire.Stop();
        madMAxFire.Clear();
    }
}
