using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DialogueAnimation : MonoBehaviour
{
    [TextArea]
    [SerializeField] private List<string> dialogue;
    public Text outputText;
    private string _output;
    private float sleepTime = 0.07f;
    private int _dialogueIdx = 0;

    #region bool
    private bool _isTalking = false;
    private bool _goGameMode = false;
    #endregion

    private void OnEnable()
    {
        StartTextAnimation();
    }

    public void StartTextAnimation()
    {
        outputText.text = "";
        _isTalking = true;
        _output = dialogue[_dialogueIdx];
        StartCoroutine(TextAnimation());
    }
    
    IEnumerator TextAnimation()
    {
        for (int i = 0; i < _output.Length; i++)
        {
            outputText.text += _output[i];
            yield return new WaitForSeconds(sleepTime);
        }
        _isTalking = false;
        if (_dialogueIdx == dialogue.Count - 1)
        {
            yield return new WaitForSeconds(1);
            _goGameMode = true;
        }
    }

    private void Update()
    {
        if (!_isTalking && gameObject.activeSelf)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && _dialogueIdx < dialogue.Count - 1)
            {
                _dialogueIdx++;
                StartTextAnimation();
            }
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && _dialogueIdx < dialogue.Count - 1)
            {
                _dialogueIdx++;
                StartTextAnimation();
            }
#endif
        }
        
        if(_goGameMode) LoadingSceneControl.LoadScene("Main");
    }
}