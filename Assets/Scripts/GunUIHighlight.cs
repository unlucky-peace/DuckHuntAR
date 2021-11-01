using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUIHighlight : MonoBehaviour
{
    //-10 10 160 160
    [SerializeField] private GameObject shotBtn;
    public bool actionStart = false;
    private Animator _gunAnimator;

    private void Start()
    {
        _gunAnimator = shotBtn.GetComponent<Animator>();
    }
    
    private void RectAction()
    {
        actionStart = false;
        /*
         수정이 필요했는데 그냥 속편하게 애니메이션 하나 만들음
        _shotRect.sizeDelta = new Vector2(Mathf.Lerp(_shotRect.rect.width, 160, 2f),
            Mathf.Lerp(_shotRect.rect.height, 160, 2f));
        _shotRect.anchoredPosition = new Vector2(Mathf.Lerp(_shotRect.anchoredPosition.x, 10, 2f),
            Mathf.Lerp(_shotRect.anchoredPosition.y, -10, 2f));
         */
        _gunAnimator.SetTrigger("Highlight");
        actionStart = false;
        DialogueManager.Instance.eventProgress = false;
    }
    
    private void Update()
    {
        if (!actionStart) return;
        shotBtn.SetActive(true);
        RectAction();
    }
}
