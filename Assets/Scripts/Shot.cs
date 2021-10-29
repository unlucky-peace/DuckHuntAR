using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    public Button btn;
    public GameObject arCamera;
    RaycastHit hit;
    void Start() => btn.onClick.AddListener(Shot_);

    /*
    private void Update()
    {
#if UNITY_EDITOR
        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;
        if(Input.GetMouseButtonDown(0)) Shot_();
#endif
    }
    */

    private void Shot_()
    {
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
                GameManager.Instance.Shot();
                if(hit.transform.CompareTag("Duck"))
                {
                    //오리 죽음
                    Debug.Log("맞음");
                    //맞았을때
                    hit.transform.GetComponent<Duck>().isDead = true;
                    GameManager.Instance.shot = 0;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Title")
            {
                if (hit.transform.CompareTag("Start"))
                {
                    LoadingSceneControl.LoadScene("Main");
                }
                
                else if (hit.transform.CompareTag("Tutorial"))
                {
                    LoadingSceneControl.LoadScene("Tuto");
                }
            }
        }
        else
        {
            GameManager.Instance.Shot();
            Debug.Log("Raycast가 안맞음");
            //쐈는데 안맞았다
        }
    }
}
