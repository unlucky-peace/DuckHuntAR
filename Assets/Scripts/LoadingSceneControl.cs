using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneControl : MonoBehaviour
{
    private static string nextScene; //불러올 씬 이름을 담을 변수
    [SerializeField] private Image progressBar; //진행 바 참조
    
    public static void LoadScene(string sceneName)
    {
        //static으로 선언 한 이유 : 그래야 처음 부터 로드 되어서 어디서든 사용 가능
        nextScene = sceneName; //받아온 씬 이름을 저장
        SceneManager.LoadScene("LoadingScene"); //로딩씬 재생
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProgress()); //Progress 애니메이션 재생
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); //다음 씬을 로딩하기 전까지 기다리는 것은 Async 메소드를 이용해
        op.allowSceneActivation = false; //로딩바 애니메이션 끝나기 전까지 씬 이동 금지

        float timer = 0f;
        while (!op.isDone)
        {
            //프로그레스 바 채워지는 애니메이션
            yield return null;
            if (op.progress < 0.09f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                {
                    timer += Time.unscaledDeltaTime;
                    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                    if (progressBar.fillAmount >= 1f)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}
