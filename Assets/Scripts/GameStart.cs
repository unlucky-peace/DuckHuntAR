using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
   public void GameModeStart() => LoadingSceneControl.LoadScene("Main");
   public void GameModeTutorial() => LoadingSceneControl.LoadScene("Tuto");
   public void AppQuit() => Application.Quit();
}
