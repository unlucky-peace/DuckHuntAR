using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    public Button btn;
    private Vector3 worldPosition;
    void Start()
    {
        btn.onClick.AddListener(Shot_);
    }

    private void Update()
    {
        //Unity 테스트용 전처리 Crosshair 움직이기
#if UNITY_EDITOR
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -3;
        transform.position = mousePos;
        if(Input.GetMouseButtonDown(0)) Shot_();
#endif
        
    }

    void Shot_()
    {
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(transform.position), transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxDistance:100))
        {
            if(hit.transform.CompareTag("Duck"))
            {
                Destroy(hit.transform.gameObject);
                Debug.Log("맞음");
                //맞았을때
                //Instantiate(smoke, hit.transform.position, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Debug.Log("나가긴함");
                //총알을 깎음
            }
        }
        else Debug.Log("Raycast가 안맞음");
    }
    
    void OnDrawGizmos() {
 
        float maxDistance = 100;
        RaycastHit hit;
        // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
        bool isHit = Physics.Raycast (transform.position, transform.forward, out hit, maxDistance);
 
        Gizmos.color = Color.red;
        if (isHit) {
            Gizmos.DrawRay (transform.position, transform.forward * hit.distance);
        } else {
            Gizmos.DrawRay (transform.position, transform.forward * maxDistance);
        }
    }
}
