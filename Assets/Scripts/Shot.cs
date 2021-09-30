using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    public Button btn;
    public GameObject arCamera;
    void Start() => btn.onClick.AddListener(Shot_);

    private void Update()
    {
        /*
#if UNITY_EDITOR
        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;
        if(Input.GetMouseButtonDown(0)) Shot_();
#endif
*/
    }

    private void Shot_()
    {
        GameManager.Instance.Shot();
        RaycastHit hit;
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if(hit.transform.CompareTag("Duck"))
            {
                //오리 죽음
                Debug.Log("맞음");
                //맞았을때
                hit.transform.GetComponent<Duck>().isDead = true;
            }
        }
        else
        {
            Debug.Log("Raycast가 안맞음");
            //쐈는데 안맞았다
        }
    }
}
