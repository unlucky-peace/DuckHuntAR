using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class Duck : MonoBehaviour
{
    //유니티 이벤트로 Duck 죽었을때 나오는거 구현
    public UnityEvent onDuckDead;
    //오리의 죽음 체크 일단 테스트용으로 public 선언 해놓음
    public bool isDead = false;
    public float speed = 0f;
    private Animator _duckAnim;
    private const string RunAway = "Runaway";

    private void Start()
    {
        _duckAnim = GetComponent<Animator>();
    }

    private void Dead()
    {
        onDuckDead.Invoke();
        Debug.Log("오리가 죽었다");
        gameObject.SetActive(false);
 
    }

    private void DuckRunAway()
    {
        _duckAnim.SetBool(RunAway, true);
    }
    private void Update()
    {
        //오리 죽으면 생기는걸 유니티 이벤트로 구현하고 싶음
        //맞춤-죽음
        if (isDead) Dead();
        //오리가 특정 방향으로 움직임
        //벽면에 닿으면 꺾음
        //10초뒤 도망
        //총알 3번 못 맞추면 도망
    }
    
}
