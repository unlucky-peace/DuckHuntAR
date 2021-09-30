using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region UI Object
    public List<GameObject> duckCount;
    public List<GameObject> heart;
    public List<GameObject> bullet;
    #endregion

    public void DuckHit()
    {
        duckCount[GameManager.Instance.hitCount - 1].GetComponent<Image>().color = Color.red;
        for (int i = 0; i < bullet.Count; i++)
        {
            bullet[i].SetActive(true);
        }
    }

    public void Shot() =>
    bullet[bullet.Count - GameManager.Instance.shot].SetActive(false);

    
    public void Damaged()
    {
        heart[heart.Count - GameManager.Instance.damage].SetActive(false);
        for (int i = 0; i < bullet.Count; i++)
        {
            bullet[i].SetActive(true);
        }
    }
    
    //스테이지 클리어시 오리 Hit 개수 초기화
    public void DuckClear()
    {
        for (int i = 0; i < duckCount.Count; i++)
        {
            duckCount[i].GetComponent<Image>().color = Color.white;
        }
    }
}
