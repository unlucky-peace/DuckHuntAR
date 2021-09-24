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
    private SpriteRenderer _duckSprite;
    private Rigidbody2D _duckRigid;
    
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
    }

    IEnumerator DuckDead()
    {
        Debug.Log("오리가 죽었다");
        _duckAnim.SetBool(Die, true);
        yield return new WaitForSeconds(1f);
        //아래로 떨어짐
        _duckRigid.MovePosition(_duckRigid.position - Vector2.down * Time.deltaTime * 10);
        /*
        if (transform.position.y < -0.02f)
        {
            _duckSprite.sortingOrder = -1;
        }
        */
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void DuckRunAway()
    {
        //위로 날아감
        _duckAnim.SetBool(RunAway, true);
    }

    private void Update()
    {
        //맞춤-죽음
        if (isDead) Dead();
        isDead = false;
        //이동
        _duckRigid.MovePosition(new Vector2(10, 10));
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
