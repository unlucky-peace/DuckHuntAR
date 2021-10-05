using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{

    #region Reference
    public GameObject shoot;
    public GameObject arCamera;
    #endregion

    
    void Start() => shoot.GetComponent<Button>().onClick.AddListener(Shot_);


    private void Shot_()
    {
        RaycastHit hit;
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if(hit.transform.CompareTag("Start"))
            {
                Debug.Log("맞음");
                GameStart_();
            }
        }
        else
        {
            Debug.Log("Raycast가 안맞음");
            //쐈는데 안맞았다
        }
    }
    
    public void GameStart_() => SceneManager.LoadScene("Main");
}
