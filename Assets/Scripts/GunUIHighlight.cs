using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUIHighlight : MonoBehaviour
{
    //-10 10 160 160
    [SerializeField] private GameObject shotBtn;
    public bool actionStart = false;
    private RectTransform _shotRect;

    private void Start()
    {
        _shotRect = shotBtn.GetComponent<RectTransform>();
    }
    
    private IEnumerator RectAction()
    {
        actionStart = false;
        yield return new WaitForSeconds(1);
        //여기 아래로 수정 필요함
        _shotRect.sizeDelta = new Vector2(Mathf.Lerp(_shotRect.rect.width, 160, 2f),
            Mathf.Lerp(_shotRect.rect.height, 160, 2f));
        _shotRect.anchoredPosition = new Vector2(Mathf.Lerp(_shotRect.anchoredPosition.x, 10, 2f),
            Mathf.Lerp(_shotRect.anchoredPosition.y, -10, 2f));
        actionStart = false;
        DialogueManager.Instance.eventProgress = false;
    }
    
    private void Update()
    {
        if (!actionStart) return;
        shotBtn.SetActive(true);
        StartCoroutine(RectAction());
    }
}
