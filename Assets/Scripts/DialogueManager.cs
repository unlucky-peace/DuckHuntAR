using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region dailogue
    [TextArea]
    [SerializeField] private List<string> dialogue;
    public Text outputText;
    private string _output;
    private const float sleepTime = 0.07f;
    private int _dialogueIdx = 0;
    #endregion
    
    #region bool
    private bool _isTalking = false;
    private bool _goGameMode = false;
    public bool eventProgress = false;
    #endregion

    #region Ref
    private GunUIHighlight _gunUIHighlight;
    #endregion

    #region singleton
    private static DialogueManager instance = null;
    #endregion
    
    private void OnEnable()
    {
        StartTextAnimation();
    }
    
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public static DialogueManager Instance => null == instance ? null : instance;

    private void Start()
    {
        _gunUIHighlight = FindObjectOfType<GunUIHighlight>();
    }

    private void StartTextAnimation()
    {
        outputText.text = "";
        _isTalking = true;
        _output = dialogue[_dialogueIdx];
        StartCoroutine(TextAnimation());
    }

    private IEnumerator TextAnimation()
    {
        foreach (var t in _output)
        {
            outputText.text += t;
            yield return new WaitForSeconds(sleepTime);
        }
        SelectEvent();
        _isTalking = false;
    }

    private void SelectEvent()
    {
        switch (_dialogueIdx)
        {
            case 2:
                eventProgress = true;
                _gunUIHighlight.actionStart = true;
                break;
            case 3:
                eventProgress = true;
                break;
            case 6:
                eventProgress = true;
                break;
            case 10:
                StartCoroutine(NextSceneAnimation());
                break;
            default:
                break;
        }
    }

    private IEnumerator NextSceneAnimation()
    {
        yield return new WaitForSeconds(1);
        _goGameMode = true;
    }
    

    private void Update()
    {
        if (!_isTalking && gameObject.activeSelf && !eventProgress)
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