using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Regen : MonoBehaviour
{

    public List<GameObject> duck; //생성 할 오리들을 담는 리스트 공간
    private int _idx = 0; //생성해야 하는 오리의 인덱스
    private Vector3 _regenPosition; //생성될 좌표

    private void Start()
    {
        StartCoroutine(DuckRegen()); //Regen 애니메이션 실행
    }

    IEnumerator DuckRegen()
    {
        while (true)
        {
            _idx = GameManager.Instance.stage - 1; //생성되는 오리는 스테이지와 같다
            if (GameManager.Instance.animationPlay || GameManager.Instance.isGameOver)
            {
                //애니메이션이 진행 중이거나 게임이 끝났으면 생성하지말고 대기(while문 조건으로도 가능)
                yield return new WaitForSeconds(3.5f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                if (!duck[_idx].activeSelf)
                {
                    //만약 오리의 active상태가 false일 경우
                    _regenPosition = new Vector3(Random.Range(-1.3f, 1.3f), -0.32f, 3); //리젠 포지션 x값만 랜덤으로
                    duck[_idx].transform.position = _regenPosition; //그렇게 정해진 포지션으로 옮겨줌
                    duck[_idx].SetActive(true); //active상태를 true로 되돌림
                    yield return new WaitForSeconds(5f);
                }
            }
        }
    }
}
