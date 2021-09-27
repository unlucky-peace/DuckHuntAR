using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Duck : MonoBehaviour
{
    //유니티 이벤트로 Duck 죽었을때 나오는거 구현
    public UnityEvent onDuckDead;
    //오리의 죽음 체크 일단 테스트용으로 public 선언 해놓음
    public bool isDead = false;
    private bool isFalling = false;
    private bool isMoving = false;
    public float speed = 0f;
    private int sign = 1;
    private Animator _duckAnim;
    private SpriteRenderer _duckSprite;
    private Rigidbody2D _duckRigid;
    
    Vector3 randomPos = Vector3.zero;
    
    private const string RunAway = "Runaway";
    private const string Die = "Die";

    private void Start()
    {
        _duckAnim = GetComponent<Animator>();
        _duckSprite = GetComponent<SpriteRenderer>();
        _duckRigid = GetComponent<Rigidbody2D>();
    }

    private void Dead()
    {
        //오리 죽으면 생기는걸 유니티 이벤트로 구현하고 싶어서 onDuckDead 이벤트에 등록된 함수들을 모두 불러옴
        onDuckDead.Invoke();
        StartCoroutine(DuckDead());
        isDead = false;
    }

    IEnumerator DuckDead()
    {
        
        Debug.Log("오리가 죽었다");
        _duckAnim.SetBool(Die, true);
        yield return new WaitForSeconds(1f);
        isFalling = true;
        yield return new WaitForSeconds(3f);
        isFalling = false;
        gameObject.SetActive(false);
    }

    private void DuckRunAway()
    {
        //위로 날아감
        _duckAnim.SetBool(RunAway, true);
    }

    private void DuckMove()
    {
        if (!isMoving)
        { 
            //randomPos = new Vector3(-1.35f, Random.Range(transform.position.y, 1.15f), 3);
        }
        _duckRigid.MovePosition((Vector2)transform.position + new Vector2(1,1) * sign * Time.deltaTime);
        if (transform.position.x <= -1.35f || transform.position.x >= 1.35f)
        {
            sign *= -1;
            Debug.Log(sign);
        }
        
        isMoving = true;
    }

    private void Update()
    {
        //맞춤-죽음
        if (isDead)
        {
            Dead();
        }
        //죽었을때 떨어짐
        if (isFalling)
        {
            _duckRigid.MovePosition(_duckRigid.position + Vector2.down * Time.deltaTime * 10);
        }
        DuckMove();
        //이동
        //벽면에 닿으면 꺾음
        //10초뒤 도망
        //총알 3번 못 맞추면 도망
        if (UIManager.Instance.shot == 3)
        {
            Debug.Log("오리야 도망가~");
            DuckRunAway();
            UIManager.Instance.shot = 0;
        }
    }
}
