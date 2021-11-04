using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    //약간 일회용 스크립트 유니티 타임라인으로 제작가능 => 튜토리얼 애니메이션은 타임라인 제작
    //게임 시작시 애니메이션 스크립트

    #region Reference
    public GameObject duckPrefab; //애니메이션에서 등장 할 오리의 프리팹을 담는 변수
    private GameObject _duck; //오리를 담을 변수
    private StageAnimation _stage; //StageAnimation.cs를 담을 공간
    #endregion

    #region String
    private const string AnStart = "Start"; //string 미리 정의
    private const string StartSound = "Start_Sound";
    #endregion

    private void Start()
    {
        //Dog Start 애니메이션 끝나고 시작함
        StartCoroutine(StartFlying());
        _stage = GetComponent<StageAnimation>(); //스테이지 애니메이션 시작을 위해 참조
    }
    
    //날아가욧
    IEnumerator StartFlying()
    {
        yield return new WaitForSeconds(4.5f); //휴지
        _duck = Instantiate(duckPrefab, new Vector3(-0.02f, -0.1f, 3), Quaternion.identity); //오리 생성
        AudioManager.instance.PlaySE(StartSound);
        yield return new WaitForSeconds(0.3f);
        AudioManager.instance.PlaySE(StartSound);
        Destroy(_duck, 2f); //생성된 오리 파괴
        yield return new WaitForSeconds(2f);
        _stage.StageAnimationStart(); //스테이지 애니메이션 재생 시작
    }
}
