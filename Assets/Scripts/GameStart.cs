using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject title;
    public GameObject crosshair;
    public GameObject shoot;
    public GameObject startTarget;
    public GameObject info;
    //public AudioClip openingSoundClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //_audioSource.clip = openingSoundClip;
    }

    public void PopUpUI()
    {
        title.SetActive(true);
        crosshair.SetActive(true);
        shoot.SetActive(true);
        startTarget.SetActive(true);
        info.SetActive(false);
        _audioSource.Play();
    }
    public void GameStart_() => SceneManager.LoadScene("Main");
}
