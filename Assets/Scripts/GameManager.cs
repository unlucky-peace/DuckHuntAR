using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public int stage = 1;
    private int _child = 0;

    public bool isGameOver = false;

    //Awake에서 instance 재 할당시 오류나는 문제 해결 바람
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public static GameManager Instance {
        get {
            if(null == instance) {
                return null;
            }
            return instance;
        }
    }

    public void StageClear()
    {
        //UIManager에서 호출 처음에는 여기서 카운팅을 하려고 했는데ㅇㅇ..
        Debug.Log("스테이지 클리어");
    }

    public void GameOver()
    {
        Debug.Log("게임오버");
        _child = UIManager.Instance.transform.childCount;
        for (int i = 0; i < _child; i++)
        {
            GameObject uiObj = UIManager.Instance.transform.GetChild(i).gameObject;
            uiObj.SetActive(!uiObj.activeSelf);
        }
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
    
}
