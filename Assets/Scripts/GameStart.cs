using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject title;
    public GameObject crosshair;
    public GameObject shoot;
    public GameObject startTarget;
    public GameObject info;
    public void PopUpUI()
    {
        title.SetActive(true);
        crosshair.SetActive(true);
        shoot.SetActive(true);
        startTarget.SetActive(true);
        info.SetActive(false);
    }
    public void GameStart_() => SceneManager.LoadScene("Main");
}
