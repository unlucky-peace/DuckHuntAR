using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        else
        {
            Debug.Log("Raycast가 안맞음");
            //이거 데미지 처리를 해야하는데 UIManager Find~로 찾아올까.. GameManager를 거치는 정말 하고 싶지 않은 코드를 짤까 고민중
            UIManager.Instance.Shot();
        }
    }
}
