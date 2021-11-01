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
    void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Main") btn.onClick.AddListener(GameSceneShot);
        else btn.onClick.AddListener(OtherSceneShot);
    }

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

    private void GameSceneShot()
    {
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
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
        else
        {
            GameManager.Instance.Shot();
            Debug.Log("Raycast가 안맞음");
            //쐈는데 안맞았다
        }
    }

    private void OtherSceneShot()
    {
        AudioManager.instance.playSE("Gun_shot");
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("TutorialDuck")) hit.collider.GetComponent<TutorialDuck>().Dead();
        }
        else
        {
            Debug.Log("Raycast가 안맞음");
            //쐈는데 안맞았다
        }
        
    }
}
