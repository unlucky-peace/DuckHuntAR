using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DuckRegen : MonoBehaviour
{

    public GameObject duckPrefab;
    private Vector3 regenPosition;
    private GameObject duck;
    void Start()
    {
        StartCoroutine(Regen());
    }
    IEnumerator Regen()
    {
        while (true)
        {
            //regenPosition = new Vector3(Random.Range(-1.5f, 1.5f), -0.7f , 3);
            //리젠 포지션 x값만 랜덤으로 
            regenPosition = new Vector3(Random.Range(-1.5f, 1.5f), 0.32f, 3);
            //오리 생성
            duck = Instantiate(duckPrefab, regenPosition, Quaternion.identity);
            //리젠용 임시 파괴 코드 파괴는 Duck 코드로ㄱㄱ
            yield return new WaitForSeconds(5);
        }
    }
}
