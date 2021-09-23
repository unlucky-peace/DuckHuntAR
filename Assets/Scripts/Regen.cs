using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regen : MonoBehaviour
{

    public List<GameObject> duck;
    void Start()
    {
        StartCoroutine(DuckRegen());
    }

    IEnumerator DuckRegen()
    {
        while (true)
        {
            //regenPosition = new Vector3(Random.Range(-1.5f, 1.5f), -0.7f , 3);
            //리젠 포지션 x값만 랜덤으로
            duck[0].SetActive(true);
            yield return new WaitForSeconds(5f);
            //regenPosition = new Vector3(Random.Range(-1.5f, 1.5f), 0.32f, 3);
        }

    }
}
