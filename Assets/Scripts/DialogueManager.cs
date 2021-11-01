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
    public int dialogueIdx = 0;
    #endregion
    
    #region bool
    private bool _isTalking = false;
    private bool _goGameMode = false;
    public bool eventProgress = false;
    #endregion

    #region Ref
    private GunUIHighlight _gunUIHighlight;
    [SerializeField] private GameObject tutorialDuck;
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
        _output = dialogue[dialogueIdx];
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
        //스위치 문쓰다가 귀찮아서 스크립투 몇 줄 줄임..
        switch (dialogueIdx)
        {
            case 2:
                eventProgress = true;
                _gunUIHighlight.actionStart = true;
                break;
            case 4:
                eventProgress = true;
                Invoke("TutorialDuckGen", 3f);
                break;
            case 7:
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

    private void TutorialDuckGen() => Instantiate(tutorialDuck,new Vector3(0, -0.32f, 3), tutorialDuck.transform.rotation);

    private void Update()
    {
        if (!_isTalking && gameObject.activeSelf && !eventProgress)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && dialogueIdx < dialogue.Count - 1)
            {
                dialogueIdx++;
                StartTextAnimation();
            }
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && dialogueIdx < dialogue.Count - 1)
            {
                dialogueIdx++;
                StartTextAnimation();
            }
#endif
        }
        
        if(_goGameMode) LoadingSceneControl.LoadScene("Main");
    }
}