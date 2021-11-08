using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; //싱글돈 지정을 위한 인스턴스
    #region UI Object
    public List<GameObject> duckCount; //잡은 오리 수 표시를 하는 UI 오브젝트를 담는 공간
    public List<GameObject> heart; //HP를 표시하는 UI 오브젝트를 담는 공간
    public List<GameObject> bullet; //총알을 표시하는 UI 오브젝트를 담는 공간
    #endregion
    
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

    public void DuckHit()
    {
        //오리가 맞았다
        duckCount[GameManager.Instance.hitCount - 1].GetComponent<Image>().color = Color.red; //빨갛게 칠하기
        foreach (var t in bullet)
        {
            t.SetActive(true); //사용한 bullet의 개수 초기화
        }
    }

    public void Shot() =>
    bullet[bullet.Count - GameManager.Instance.shot].SetActive(false); //총알을 쐈을때 총알의 개수 차감

    
    public void Damaged()
    {
        //플레이어가 데미지를 입었을때
        heart[heart.Count - GameManager.Instance.damage].SetActive(false);
        foreach (var t in bullet)
        {
            t.SetActive(true); //사용한 bullet의 개수 초기화
        }
    }
    

    public void DuckClear()
    {
        //스테이지 클리어시 오리 Hit 개수 초기화
        foreach (var t in duckCount)
        {
            t.GetComponent<Image>().color = Color.white;
        }
    }

    public void Recover()
    {
        //스테이지 클리어시 HP 복구
        foreach (var t in heart)
        {
            t.SetActive(true); //HP 복구
        }
    }
}
