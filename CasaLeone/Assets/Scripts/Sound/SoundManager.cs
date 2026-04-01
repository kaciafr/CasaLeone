using System;
using UnityEngine;
using System.Collections;

public enum SoundType
{
   Background,
   FootStep,
   PropsCook 
}
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
   [SerializeField] private SoundList[] soundsList;
   private static SoundManager instance;
   private AudioSource audioSource;

   private void Awake()
   {
      instance = this; 
   }
   private void Start()
   {
      audioSource = GetComponent<AudioSource>();
   }
   public static void PlaySound(SoundType soundType, float volume = 1.0f)
   {
      AudioClip[] clips = instance.soundsList[(int)soundType].Sounds;
      AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
      instance.audioSource.PlayOneShot(randomClip, volume);
   }
   private void OnEnable()
   {
      string[] names = Enum.GetNames(typeof(SoundType));
      Array.Resize(ref soundsList, names.Length);
      for (int i = 0; i < soundsList.Length; i++)
      {
         soundsList[i].name = names[i];
      }
   }
   [System.Serializable]
   public struct SoundList
   {
      [HideInInspector] public string name; 
      public AudioClip[] Sounds
      { get => sounds; }
      [SerializeField]private AudioClip[] sounds;
   }
}
