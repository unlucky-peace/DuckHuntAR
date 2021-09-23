using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<GameObject> duck;
    private int _hitCount = 0;

    public void DuckHit()
    {
        duck[_hitCount].GetComponent<Image>().color = Color.red;
        _hitCount++;
    }
}
