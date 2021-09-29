using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Regen : MonoBehaviour
{

    public List<GameObject> duck;
    private Vector3 _regenPosition;
    void Start()
    {
        StartCoroutine(DuckRegen());
    }

    IEnumerator DuckRegen()
    {
        while (true)
        {
            if (GameManager.Instance.stageAnimationPlay || GameManager.Instance.isGameOver)
            {
                yield return new WaitForSeconds(3.5f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
                if (!duck[0].activeSelf)
                {
                    _regenPosition = new Vector3(Random.Range(-1.3f, 1.3f), -0.32f, 3);
                    //리젠 포지션 x값만 랜덤으로
                    duck[0].transform.position = _regenPosition;
                    duck[0].SetActive(true);
                    yield return new WaitForSeconds(5f);
                }
            }
        }
    }
}
