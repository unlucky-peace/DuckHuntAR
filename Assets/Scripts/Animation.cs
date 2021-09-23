using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    //약간 일회용 스크립트 굳이 이렇게까지 해야하나 싶지만 일단 나중에 리팩토링 하는걸로 하고 임시로 만들어놓음
    //게임 시작시 애니메이션
    public GameObject duckPrefab;
    private Animator _duckAnim;
    private GameObject _duck;
    private const string AnStart = "Start";
    private void Start()
    {
        //Dog Start 애니메이션 끝나고 시작함
        StartCoroutine(StartFlying());
    }
    
    //날아가욧
    IEnumerator StartFlying()
    {
        //공백
        yield return new WaitForSeconds(3f);
        //오리 생성
        _duck = Instantiate(duckPrefab, new Vector3(-0.02f, -0.1f, 3), Quaternion.identity);
        //오리 애니메이션 재생을 위해 Animator 얻어오기
        _duckAnim = _duck.GetComponent<Animator>();
        _duckAnim.SetTrigger(AnStart);
    }
}
