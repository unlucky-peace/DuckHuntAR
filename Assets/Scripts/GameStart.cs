using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    //public AudioClip openingSoundClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //_audioSource.clip = openingSoundClip;
        _audioSource.Play();
    }
    
    //public void GameStart_() => SceneManager.LoadScene("Main");
}
