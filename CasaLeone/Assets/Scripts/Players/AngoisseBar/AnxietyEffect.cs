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
    private ChromaticAberration chromaticAberration;
    private FilmGrain filmGrain;
    private LensDistortion distortion;
    private ColorAdjustments colorAdjustments;
    
    private float targetVignette;
    private float targetChromaticAberration;
    private float targetFilmGrain;
    private float targetLensDistortion;
    private float targetSaturation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out filmGrain);
        volume.profile.TryGet(out distortion);
        volume.profile.TryGet(out colorAdjustments);
    }
    


    void Update()
    {
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetVignette, Time.deltaTime * transitionSpeed);
        }

        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, targetChromaticAberration, Time.deltaTime * transitionSpeed);
        }

        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = Mathf.Lerp(colorAdjustments.saturation.value, targetSaturation, Time.deltaTime * transitionSpeed);
        }

        if (filmGrain != null)
        {
            filmGrain.intensity.value = Mathf.Lerp(filmGrain.intensity.value, targetFilmGrain, Time.deltaTime * transitionSpeed);
        }

        if (distortion != null)
        {
            distortion.intensity.value = Mathf.Lerp(distortion.intensity.value, targetLensDistortion,
                Time.deltaTime * transitionSpeed); 
        }
}
    
    public void SetNormal()
    {
        targetVignette = 0.2f;
        targetChromaticAberration = 0f;
        targetSaturation = 0f;
        targetFilmGrain = 0.1f;
        targetLensDistortion = 0f;
    }

    public void SetLightStress()
    {
        targetVignette = 0.4f;
        targetChromaticAberration = 0.2f;
        targetSaturation = -20f;
        targetFilmGrain = 0.3f;
        targetLensDistortion = -0.1f;
    }
    
    public void SetHightLightStress()
    {
        targetVignette = 0.6f;
        targetChromaticAberration = 0.5f;
        targetSaturation = -50f;
        targetFilmGrain = 0.6f;
        targetLensDistortion = -0.3f;
    }

    public void SetMadMaxStress()
    {
        targetVignette = 0.8f;
        targetChromaticAberration = 1f;
        targetSaturation = -100f;
        targetFilmGrain = 1f;
        targetLensDistortion = -0.6f;    
        
    }
    
}
