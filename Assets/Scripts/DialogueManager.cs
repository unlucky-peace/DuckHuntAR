using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region dailogue
    [TextArea]
    [SerializeField] private List<string> dialogue; //log를 미리 작성해 놓을 리스트
    public Text outputText; //log를 출력 할 오브젝트
    private string _output; //실제 내보낼 대사
    private const float sleepTime = 0.07f; //글자 사이 휴지 간격
    public int dialogueIdx = 0; //출력해야 하는 log의 인덱스
    #endregion
    
    #region bool
    private bool _isTalking = false; //지금 말하는 중이면 클릭해도 다음 대사로 넘어가지 않음
    private bool _goGameMode = false; //튜토리얼이 종료 되어서 게임 모드로 넘어가야함
    public bool eventProgress = false; //이벤트(슛 버튼 애니메이션, 오리 생성)중인가?
    #endregion

    #region Ref
    private GunUIHighlight _gunUIHighlight; //Shoot 버튼 이벤트를 위한 참조
    [SerializeField] private GameObject tutorialDuck; //생성할 오리의 프리팹이 들어 갈 변수
    #endregion

    #region singleton
    private static DialogueManager instance = null; //싱글톤 지정을 위한 인스턴스
    #endregion
    
    private void OnEnable()
    {
        //애니메이션 진행시 잠깐 Dialogue를 끔, 따라서 Start가 아니라 OnEnable에서 메소드 실행
        StartTextAnimation();
    }
    
    private void Awake() {
        //싱글톤 인스턴스 지정
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public static DialogueManager Instance => null == instance ? null : instance;

    private void Start()
    {
        //FindObject로 스크립트 참조
        _gunUIHighlight = FindObjectOfType<GunUIHighlight>();
    }

    private void StartTextAnimation()
    {
        outputText.text = ""; //이전 텍스트 초기화
        _isTalking = true; //말하는 중
        _output = dialogue[dialogueIdx]; //출력할 log 할당
        StartCoroutine(TextAnimation()); //TextAnimation실행
    }

    private IEnumerator TextAnimation()
    {
        foreach (var t in _output)
        {
            //_output 스트링을 탐색하면서 한 단어씩 sleepTime 시간 대기 후 출력
            outputText.text += t;
            yield return new WaitForSeconds(sleepTime);
        }
        SelectEvent(); //메소드 실행
        _isTalking = false; //말하기 종료
    }

    private void SelectEvent()
    {
        //dialogueIdx에 따라 이벤트 실행
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
    
    //튜토리얼용 오리 생성
    private void TutorialDuckGen() => Instantiate(tutorialDuck,new Vector3(0, -0.32f, 3), tutorialDuck.transform.rotation);

    private void Update()
    {
        //사용자 터치를 받아와서 Script를 다음으로 넘김
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
        
        //게임 모드가 활성화 되면 Main씬으로 이동
        if(_goGameMode) LoadingSceneControl.LoadScene("Main");
    }
}