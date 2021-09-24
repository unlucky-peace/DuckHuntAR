using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    //Dog 애니메이터 담을 공간
    private Animator dogAnim;
    //Dog SpriteRendere 담을 공간
    private SpriteRenderer dogSprite;
    
    //애니메이션 재생시 사용하는 문자열 미리 저장(Extension 생각)
    private String start = "Start";
    private String laugh = "Laugh";
    private String hunt = "Hunt_";
    

    void Start()
    {
        //컴포넌트 받아오기
        dogAnim = GetComponent<Animator>();
        dogSprite = GetComponent<SpriteRenderer>();
        //Round 시작시 애니메이션 재생
        StartCoroutine(DogStart());
    }
    
    //Round 시작시 나오는 애니메이션
    IEnumerator DogStart()
    {
        //enabled = false 상태의 Sprite를 켜준다
        dogSprite.enabled = true;
        //애니메이션 재생
        dogAnim.SetBool(start, true);
        //애니메이션 끝날때 까지 기다림
        yield return new WaitForSeconds(3f);
        //Sprite 끄기
        dogSprite.enabled = false;
        //애니메이션 Idle 상태로 되돌리기
        dogAnim.SetBool(start, false);
        
        //테스트용 코드
        //StartCoroutine(DogLaugh());
    }
    IEnumerator DogLaugh()
    {
        yield return new WaitForSeconds(1f);
        dogSprite.enabled = true;
        dogAnim.SetBool(laugh, true);
        yield return new WaitForSeconds(2.5f);
        dogSprite.enabled = false;
        dogAnim.SetBool(laugh, false);
    }

    IEnumerator DogHunt()
    {
        yield return new WaitForSeconds(2f);
        dogSprite.enabled = true;
        //hunt 정보 받아와서 1, 2 번 재생 선택하기
        dogAnim.SetBool(hunt + 1, true);
        yield return new WaitForSeconds(3f);
        dogAnim.SetBool(hunt + 1, false);
        dogSprite.enabled = false;
    }
    
    //코루틴은 유니티 이벤트에 등록할 수 없어서 함수를 쪼갬
    public void Hunt()
    {
        //이부분 애니메만들어주세요 만들었어요 코루틴으로 만들고 공백주고 이벤트 등록해주세요~
        StartCoroutine(DogHunt());
    }
}
