using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private static GameManager instance = null; //인스턴스

    #region bool Type
    public bool isGameOver = false; //게임 오버 상태인가?
    public bool animationPlay = true; //애니메이션이 플레이 중인가?
    #endregion

    #region Reference
    private StageAnimation _stageScript; //StageAnimation 클래스 참조
    private Duck _duck; //Duck 클래스 참조
    public TextMeshProUGUI timeT; //시간을 표시하는 UI 오브젝트를 담는 공간
    [SerializeField] private TextMeshProUGUI gameOverT; //게임 오버 텍스트를 표시하는 UI 오브젝트를 담는 공간
    [SerializeField] private Button restartBtn; //타이틀로 돌아가는 버튼 UI 오브젝트를 담는 공간
    [SerializeField] private Button exitBtn; //게임 종료 버든 UI 오브젝트를 담는 공간
    #endregion

    #region value
    public int stage = 1; //기본 스테이지
    private int _child = 0; //UI에 달린 자식 오브젝트 개수를 담는 공간
    public int hitCount = 0; //오리를 얼마나 잡았는지 기록하는 변수
    public int shot = 0; //발사 횟수를 기록하는 변수
    public int damage = 0; //데미지를 얼마나 받았는지 기록하는 변수
    private const string shootSound = "Gun_shot"; //애니메이션 string 미리 지정
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
        _stageScript.StageAnimationStart(); //스테이지 클리어 했을때 다음 스테이지 시작 애니메이션 재생
    }

    private void GameOver()
    {
        Debug.Log("게임오버");
        gameOverT.text = "Game Over";
        AudioManager.instance.PlaySE(gameOverSound); //게임 오버 사운드
        _child = UIManager.instance.transform.childCount; //UIManager에 달린 자식 오브젝트 개수 Count
        for (var i = 0; i < _child; i++)
        {
            //그 오브젝트들이 꺼져있으면 켜고, 꺼져있으면 켜라
            var uiObj = UIManager.instance.transform.GetChild(i).gameObject;
            uiObj.SetActive(!uiObj.activeSelf);
        }
    }

    private void GameClear()
    {
        // GameOver() 메소드와 유사
        Debug.Log("게임 클리어");
        gameOverT.text = "Game Clear!";
        _child = UIManager.instance.transform.childCount;
        for (var i = 0; i < _child; i++)
        {
            var uiObj = UIManager.instance.transform.GetChild(i).gameObject;
            uiObj.SetActive(!uiObj.activeSelf);
        }
    }
    
    //게임 종료시 타이틀로
    public void ReturnToTitle()
    {
        //켜진 오브젝트를 다시 모두 꺼주고
        gameOverT.gameObject.SetActive(false);
        restartBtn.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);
        //게임 오버 상태에서 벗어난 다음
        isGameOver = false;
        //Title씬으로 회귀
        LoadingSceneControl.LoadScene("Title");
    }
    
    //게임 종료시 어플리케이션 off
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    //GameEvent
    public void Shot()
    {
        //발사 했을 때
        shot++;
        AudioManager.instance.PlaySE(shootSound);
        UIManager.instance.Shot();
    }

    public void DuckHit()
    {
        //오리가 맞았을 때
        hitCount++;
        UIManager.instance.DuckHit();
    }

    public void Damaged()
    {
        //Hp가 깎였을 때
        damage++;
        UIManager.instance.Damaged();
        shot = 0;
    }
    
    private void Update()
    {
        if (isGameOver) return;
        if (stage == 3)
        {
            damage = 0;
            GameClear();
            isGameOver = true;
        }
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
