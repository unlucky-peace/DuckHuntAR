using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    #region bool Type
    public bool isGameOver = false;
    public bool animationPlay = true;
    #endregion

    #region Reference
    private StageAnimation _stageScript;
    private Duck _duck;
    public TextMeshProUGUI timeT;
    public TextMeshProUGUI clearT;
    #endregion

    #region value
    public int stage = 1;
    private int _child = 0;
    public int hitCount = 0;
    public int shot = 0;
    public int damage = 0;
    #endregion


    //Awake에서 instance 재할당시 오류나는 문제 해결 바람
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
        _stageScript = GetComponent<StageAnimation>();
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
        Debug.Log("스테이지 클리어");
        stage++;
        if(stage > 3) GameClear();
        UIManager.Instance.DuckClear();
        animationPlay = true;
        _stageScript.StageAnimationStart();
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

    public void GameClear()
    {
        Debug.Log("게임 클리어");
        _child = UIManager.Instance.transform.childCount;
        for (int i = 0; i < _child; i++)
        {
            GameObject uiObj = UIManager.Instance.transform.GetChild(i).gameObject;
            uiObj.SetActive(false);
        }
        
        clearT.gameObject.SetActive(true);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
        isGameOver = false;
    }
    
    //GameEvent
    public void Shot()
    {
        shot++;
        UIManager.Instance.Shot();
    }

    public void DuckHit()
    {
        hitCount++;
        UIManager.Instance.DuckHit();
    }

    public void Damaged()
    {
        damage++;
        Debug.Log("Damaged 호출");
        UIManager.Instance.Damaged();
        shot = 0;
    }
    
    private void Update()
    {
        if (!isGameOver)
        {
            if (hitCount == 10)
            {
                GameManager.Instance.StageClear();
                hitCount = 0;
            }

            if (shot == 3)
            {
                Debug.Log("피깎임");
                _duck = FindObjectOfType<Duck>();
                _duck.Runaway();
            }
            
            if (damage == 3)
            {
                damage = 0;
                GameManager.Instance.GameOver();
                GameManager.Instance.isGameOver = true;
            }
        }
    }
    
}
