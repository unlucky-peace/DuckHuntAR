using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
   //Title 화면에서 버튼 액션
   public void GameModeStart() => LoadingSceneControl.LoadScene("Main");
   public void GameModeTutorial() => LoadingSceneControl.LoadScene("Tuto");
   public void AppQuit() => Application.Quit();
}
