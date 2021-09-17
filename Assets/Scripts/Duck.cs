using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    //오리의 죽음 체크 일단 테스트용으로 public 선언 해놓음
    public bool isDead = false;
    void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        //오리가 특정 방향으로 움직임
        //벽면에 닿으면 꺾음
        //10초뒤 도망
        //총알 3번 못 맞추면 도망
        //맞춤-죽음
    }
}
