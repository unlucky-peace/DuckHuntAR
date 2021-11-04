using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageAnimation : MonoBehaviour
{
    //Stage시작할때 실행하는 애니메이션을 코드로 제어함
    public TextMeshProUGUI stageText; //출력
    private float sleepTime = 0.3f; //휴지시간
    [SerializeField] private string output = ""; //출력 할 text를 담을 공간
    private const string stageClearSound = "Stage_clear"; //sound resource name 미리 정의
    private const string dogBarkSound = "Dog_bark";
    
    public void StageAnimationStart()
    {
        //스테이지 시작 애니메이션 코루틴 실행 시키기 위한 메소드
        StartCoroutine(StageAnimationCor());
    }

    private void TextAnimationStart()
    {
        //텍스트 애니메이션 코루틴 실행을 위한 메소드
        StartCoroutine(TextAnimation());
    }

    IEnumerator StageAnimationCor()
    {
        GameManager.Instance.animationPlay = true; //애니메이션 재생이 시작되었음을 알려줌
        AudioManager.instance.PlaySE(stageClearSound); //스테이지 시작 브금
        yield return new WaitForSeconds(1.5f); //휴지
        UIManager.instance.DuckClear(); //스테이지가 시작 되면 UI 부분의 오리를 초기화 시켜줌
        TextAnimationStart(); //스테이지를 알려주는 텍스트 코루틴 호출
        //텍스트 애니메이션 재생 동안 기다림
        yield return new WaitForSeconds(sleepTime * (output.Length + 1) + 1); //아 여기 async로 하거나 그냥 아예 타임라인으로 해야 편한데
        AudioManager.instance.PlaySE(dogBarkSound); 
        yield return new WaitForSeconds(sleepTime);
        AudioManager.instance.PlaySE(dogBarkSound);
        GameManager.Instance.animationPlay = false; //애니메이션 재생이 끝났음을 알려줌
    }

    IEnumerator TextAnimation()
    {
        for (int i = 0; i < output.Length; i++)
        {
            //해당 스크립트는 Dialogue의 텍스트 출력과 동일
            stageText.text += output[i];
            yield return new WaitForSeconds(sleepTime);
        }
        //현재 씬의 이름에 따라 출력되는 string이 달라짐
        if (SceneManager.GetActiveScene().name == "Main")
        {
            stageText.text += GameManager.Instance.stage;
            yield return new WaitForSeconds(sleepTime + 1);
            stageText.text = "";
        }
        else
        {
            yield return new WaitForSeconds(1f);
            stageText.text = "";
        }

    }
}
