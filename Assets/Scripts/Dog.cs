using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    #region Reference
    //Dog 애니메이터 담을 공간
    private Animator _dogAnim;
    //Dog SpriteRendere 담을 공간
    private SpriteRenderer _dogSprite;
    #endregion
    
    #region string
    //애니메이션 재생시 사용하는 문자열 미리 저장(Extension 생각)
    private const string start = "Start";
    private const string laugh = "Laugh";
    private const string hunt = "Hunt_";
    private const string laughSound = "Dog_laugh";
    private const string huntSound = "Dog_hunt";
    #endregion

    void Start()
    {
        //컴포넌트 받아오기
        _dogAnim = GetComponent<Animator>();
        _dogSprite = GetComponent<SpriteRenderer>();
        //Round 시작시 애니메이션 재생
        StartCoroutine(DogStart());
    }
    
    //Round 시작시 나오는 애니메이션
    IEnumerator DogStart()
    {
        yield return new WaitForSeconds(1.5f);
        //enabled = false 상태의 Sprite를 켜준다
        _dogSprite.enabled = true;
        //애니메이션 재생
        _dogAnim.SetBool(start, true);
        //애니메이션 끝날때 까지 기다림
        yield return new WaitForSeconds(3f);
        //Sprite 끄기
        _dogSprite.enabled = false;
        //애니메이션 Idle 상태로 되돌리기
        _dogAnim.SetBool(start, false);
    }
    
    IEnumerator DogLaugh()
    {
        GameManager.Instance.animationPlay = true;
        yield return new WaitForSeconds(1f);
        _dogSprite.enabled = true;
        _dogAnim.SetBool(laugh, true);
        AudioManager.instance.PlaySE(laughSound);
        GameManager.Instance.animationPlay = false;
        yield return new WaitForSeconds(2.5f);
        _dogAnim.SetBool(laugh, false);
        _dogSprite.enabled = false;
    }

    IEnumerator DogHunt()
    {
        GameManager.Instance.animationPlay = true;
        yield return new WaitForSeconds(2f);
        _dogSprite.enabled = true;
        //hunt 정보 받아와서 1, 2 번 재생 선택하기
        _dogAnim.SetBool(hunt + 1, true);
        GameManager.Instance.animationPlay = false;
        AudioManager.instance.PlaySE(huntSound);
        yield return new WaitForSeconds(3f);
        _dogAnim.SetBool(hunt + 1, false); 
        _dogSprite.enabled = false;
    }
    
    //코루틴은 유니티 이벤트에 등록할 수 없어서 함수를 쪼갬
    public void Hunt()
    {
        //이부분 애니메만들어주세요 만들었어요 코루틴으로 만들고 공백주고 이벤트 등록해주세요~
        StartCoroutine(DogHunt());
    }

    public void Laugh()
    {
        StartCoroutine(DogLaugh());
    }
}
