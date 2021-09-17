using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using  TMPro;
using UnityEditor;

public class DetectPlane : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;

    #region UIObject
    public GameObject start;
    public GameObject title;
    public GameObject crosshair;
    public GameObject shot;
    public GameObject lookat;
    #endregion
    
    void Start()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }
    
    void Update()
    {
        DetectGround();
    }

    private void DetectGround()
    {
        //스크린의 중앙지점
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        
        //레이에 부딪힌 대상들의 정보를 저장할 리스트 변수
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        
        //만약, 스크린의 중앙 지점에서 레이를 발사했을때 Plane 타입 추적 대상이 있다면,
        if (_arRaycastManager.Raycast(screenSize, hits, TrackableType.Planes))
        {
            //UI, BGM 켜기 안내 문구 지워주세요
            lookat.SetActive(false);
            start.SetActive(true); 
            title.SetActive(true);
            shot.SetActive(true);
            crosshair.SetActive(true);
        }
        //그렇지 않다면 없어
    }
}
