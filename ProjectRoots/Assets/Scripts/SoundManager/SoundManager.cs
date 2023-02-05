using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource musicSource_ , effectsSource_;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        musicSource_.Play();
    }    

    public void PlaySound(AudioClip clip, float volumeScale)
    {
        effectsSource_.PlayOneShot(clip, volumeScale);
    }
}
