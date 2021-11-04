using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUIHighlight : MonoBehaviour
{
    //-10 10 160 160
    [SerializeField] private GameObject shotBtn; //하이라이트 할 Shoot 버튼 담을 변수
    public bool actionStart = false; //액션 시작 신호
    private Animator _gunAnimator; //버튼에 저장된 애니메이션 재생을 위한 애니메이터 참조

    private void Start()
    {
        _gunAnimator = shotBtn.GetComponent<Animator>();
    }
    
    private void RectAction()
    {
        actionStart = false; //trigger니까 바로 false로 아니면 업데이트에서 계속 실행됨
        /*
         수정이 필요했는데 그냥 속편하게 애니메이션 하나 만들음
        _shotRect.sizeDelta = new Vector2(Mathf.Lerp(_shotRect.rect.width, 160, 2f),
            Mathf.Lerp(_shotRect.rect.height, 160, 2f));
        _shotRect.anchoredPosition = new Vector2(Mathf.Lerp(_shotRect.anchoredPosition.x, 10, 2f),
            Mathf.Lerp(_shotRect.anchoredPosition.y, -10, 2f));
         */
        _gunAnimator.SetTrigger("Highlight"); //하이라이트 애니메이션 재생
        DialogueManager.Instance.eventProgress = false; //이벤트 끝났다고 신호 보내줌
    }
    
    private void Update()
    {
        if (!actionStart) return; //액션 Start가 false면 아래 스크립트 실행하지 않고 돌아감
        shotBtn.SetActive(true); //버튼 활성화
        RectAction(); //애니메이션 재생을 위한 메소드 호출
    }
}
