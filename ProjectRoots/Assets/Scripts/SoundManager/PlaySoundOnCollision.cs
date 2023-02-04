using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{

    public AudioClip objSound;

    [Header("ChangeBetween 0-1")]
    public float audioVolume;

    private void OnCollisionEnter(Collision collision)
    {        
        SoundManager.instance.PlaySound(objSound,audioVolume);
    }
}
