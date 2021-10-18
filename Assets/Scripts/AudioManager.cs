using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; //오디오 클립 이름으로 재생
    public AudioClip clip; //실제 재생할 오디오 클립
}
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    #region 싱글톤
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);
    }
    #endregion

    public AudioSource[] effectAudioSource;
    public AudioSource bgmAudioSource;

    public string[] playSoundName;

    public Sound[] effect;
    public Sound bgm;

    private void Start()
    {
        playSoundName = new string[effectAudioSource.Length];
    }

    public void playSE(string _name)
    {
        for (int i = 0; i < effect.Length; i++)
        {
            if (_name == effect[i].name)
            {
                for (int j = 0; j < effectAudioSource.Length; j++)
                {
                    if (!effectAudioSource[j].isPlaying)
                    {
                        playSoundName[j] = effect[i].name;
                        effectAudioSource[j].clip = effect[i].clip;
                        effectAudioSource[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 오디오 소스 사용중");
                return;
            }
        }
        Debug.Log(_name + " 사운드가 사운드 매니저에 등록 되지 않았습니다.");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < effectAudioSource.Length; i++)
        {
            effectAudioSource[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < effectAudioSource.Length; i++)
        {
            if(playSoundName[i] == _name) effectAudioSource[i].Stop();
            return;
        }
        Debug.Log("재생중인" + _name + "사운드가 없습니다");
    }
}
