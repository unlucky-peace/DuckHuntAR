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
    [SerializeField] private TextMeshProUGUI gameOverT;
    #endregion

    #region value
    public int stage = 1;
    private int _child = 0;
    public int hitCount = 0;
    public int shot = 0;
    public int damage = 0;
    private const string shootSound = "Gun_shot";
    private const string gameOverSound = "Game_over";
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
        _stageScript.StageAnimationStart();
    }

    public void GameOver()
    {
        Debug.Log("게임오버");
        gameOverT.text = "Game Over";
        AudioManager.instance.playSE(gameOverSound);
        _child = UIManager.instance.transform.childCount;
        for (int i = 0; i < _child; i++)
        {
            GameObject uiObj = UIManager.instance.transform.GetChild(i).gameObject;
            uiObj.SetActive(!uiObj.activeSelf);
        }
    }

    public void GameClear()
    {
        Debug.Log("게임 클리어");
        gameOverT.text = "Game Clear!";
        _child = UIManager.instance.transform.childCount;
        for (int i = 0; i < _child; i++)
        {
            GameObject uiObj = UIManager.instance.transform.GetChild(i).gameObject;
            uiObj.SetActive(false);
        }
        
        gameOverT.gameObject.SetActive(true);
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
        AudioManager.instance.playSE(shootSound);
        UIManager.instance.Shot();
    }

    public void DuckHit()
    {
        hitCount++;
        UIManager.instance.DuckHit();
    }

    public void Damaged()
    {
        damage++;
        Debug.Log("Damaged 호출");
        UIManager.instance.Damaged();
        shot = 0;
    }
    
    private void Update()
    {
        if (!isGameOver)
        {
            if (hitCount == 10)
            {
                Invoke("StageClear", 3f);
                animationPlay = true;
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
                GameOver();
                isGameOver = true;
            }
        }
    }
    
}
