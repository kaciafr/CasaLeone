using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AnxietyEffect : MonoBehaviour
{
    public static AnxietyEffect instance;
    public Volume volume;
    public float transitionSpeed;
    
    private Vignette vignette;
    
    private float targetVignette;

    void Awake() 
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (volume == null)
        {
            Debug.LogError("Volume non assigné sur AnxietyEffect !");
            return;
        }

        volume.profile.TryGet(out vignette);
    }


    void Update()
    {
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetVignette, Time.deltaTime * transitionSpeed);
        }

}
    
    public void SetNormal()
    {
        targetVignette = 0.2f;
        
    }

    public void SetLightStress()
    {
        targetVignette = 0.4f;
       
    }
    
    public void SetHightStress()
    {
        targetVignette = 0.6f;
        
    }

    public void SetMadMaxStress()
    {
        targetVignette = 0.8f;
         
        
    }
    
}
