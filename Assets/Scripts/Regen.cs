using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Regen : MonoBehaviour
{

    public List<GameObject> duck;
    private int idx = 0;
    private Vector3 _regenPosition;
    
    void Start()
    {
        StartCoroutine(DuckRegen());
    }

    IEnumerator DuckRegen()
    {
        while (true)
        {
            idx = GameManager.Instance.stage - 1;
            if (GameManager.Instance.animationPlay || GameManager.Instance.isGameOver)
            {
                yield return new WaitForSeconds(3.5f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                if (!duck[idx].activeSelf)
                {
                    _regenPosition = new Vector3(Random.Range(-1.3f, 1.3f), -0.32f, 3);
                    //리젠 포지션 x값만 랜덤으로
                    duck[idx].transform.position = _regenPosition;
                    duck[idx].SetActive(true);
                    yield return new WaitForSeconds(5f);
                }
            }
        }
    }
}
