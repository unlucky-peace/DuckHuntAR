using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();

            return instance;
        }
    }
    #region UI Object
    [SerializeField] private List<GameObject> duckCount;
    [SerializeField] private List<GameObject> heart;
    [SerializeField] private List<GameObject> bullet;
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
