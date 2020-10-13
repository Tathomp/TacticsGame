using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MusicManager : MonoBehaviour
{

    [SerializeField]
    AudioSource player;

    private void Awake()
    {
        InitializePlayer();
        
    }


    public void ChangeSong(string musicKey)
    {
        AudioClip sound = Resources.Load<AudioClip>(musicKey);

        player.clip = sound;
        player.Play();
    }


    public abstract void InitializePlayer();
}
