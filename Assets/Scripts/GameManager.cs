using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public TextMeshProUGUI timeT;
    public GameObject[] huntingCount = new GameObject[10];

    private int hunt = 0;
    private float _time = 10f;
    //Awake에서 instance 재 할당시 오류나는 문제 해결 바람
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public static GameManager Instance {
        get {
            if(null == instance) {
                return null;
            }
            return instance;
        }
    }

    private void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            Debug.Log("시간초과");
            _time = 10;
        }
        else timeT.text = "Time \n" + Mathf.RoundToInt(_time);
    }
}
