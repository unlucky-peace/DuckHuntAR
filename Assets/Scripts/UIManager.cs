using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public List<GameObject> duck;
    public List<GameObject> heart;
    public List<GameObject> bullet;
    public TextMeshProUGUI timeT;
    private int _hitCount = 0;
    public int shot = 0;
    private int _damage = 0;


    private void Awake() {
        instance = this;
    }
    public static UIManager Instance {
        get {
            if(null == instance) {
                return null;
            }
            return instance;
        }
    }

    public void DuckHit()
    {
        duck[_hitCount].GetComponent<Image>().color = Color.red;
        _hitCount++;
        //이거 일단 임시 코드.. 총알 깎인 상태에서 오리 죽이면 총알 복구 되어야 하는데 코드를 어디다 작성해야할지 고민해보자
        for (int i = 0; i < bullet.Count; i++)
        {
            bullet[i].SetActive(true);
        }
    }

    public void Shot() 
    {
        //이거 값 받아와서 조절가능하나? 일단 호출은 Shot에서 하고 있음
        bullet[(bullet.Count - 1) - shot].SetActive(false);
        shot++;
    }

    public void Damaged()
    {
        heart[(heart.Count - 1) - _damage].SetActive(false);
        _damage++;
        for (int i = 0; i < bullet.Count; i++)
        {
            bullet[i].SetActive(true);
        }
        shot = 0;
    }

    private void Update()
    {
        //아 극혐
        if (!GameManager.Instance.isGameOver)
        {
            if (_hitCount == 10)
            {
                GameManager.Instance.StageClear();
                _hitCount = 0;
            }

            if (shot == 3)
            {
                Debug.Log("피깎임");
                Damaged();
                //여기서.. 오리를 도망가게하는 코드를 짜야하는데 아 Duck 스크립트를 찾아오면 버그가 있을것 같고 GameManager경유하기는 싫고
                //일단은 Duck 스크립트에서 값을 매번 검사하는 형태로
            }
            
            if (_damage == 3)
            {
                _damage = 0;
                GameManager.Instance.GameOver();
                GameManager.Instance.isGameOver = true;
            }
        }
    }
}
