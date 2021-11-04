using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    [SerializeField] private Button btn; //Shot 버튼 참조
    [SerializeField] private GameObject arCamera; //발사를 위한 카메라 참조
    private RaycastHit _hit; //RaycastHit 정보를 담을 변수

    private void OnEnable()
    {
        //총을 쏘는 씬 2개 씬 네임에 따라 버튼에 붙는 메소드 변경(상속으로 스크립트 분리 가능)
        if(SceneManager.GetActiveScene().name == "Main") btn.onClick.AddListener(GameSceneShot);
        else btn.onClick.AddListener(OtherSceneShot);
    }

    /* PC환경 테스트용 업데이트
    private void Update()
    {
#if UNITY_EDITOR
        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;
        if(Input.GetMouseButtonDown(0)) Shot_();
#endif
    }*/
    
    //게임 씬에서 사용하는 Shot 메소드
    private void GameSceneShot()
    {
        GameManager.Instance.Shot(); // 발사 처리
        //Raycast 발사
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out _hit))
        {
            if (!_hit.transform.CompareTag("Duck")) return; //뭔가 맞았는데 그 오브브젝트의 Tag가 Duck이 아니면 리턴
            Debug.Log("맞음"); //테스트용 디버그
            _hit.transform.GetComponent<Duck>().isDead = true; //오리의 죽음 처리
            GameManager.Instance.shot = 0; //발사 횟수 초기화
        }
        else
        {
            Debug.Log("Raycast가 안맞음");
        }
    }
    
    //게임 씬을 제외한 씬에서 사용하는 메소드
    private void OtherSceneShot()
    {
        AudioManager.instance.playSE("Gun_shot");
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out _hit))
        {
            if (_hit.transform.CompareTag("TutorialDuck")) _hit.collider.GetComponent<TutorialDuck>().Dead();
        }
        else
        {
            Debug.Log("아무것도 안맞음");
        }
        
    }
}
