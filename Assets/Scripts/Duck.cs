using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Duck : MonoBehaviour
{
    //유니티 이벤트
    public UnityEvent onDuckDead; //오리가 죽었을때 일어나는 이벤트
    public UnityEvent onDuckRunaway; //오리가 도망갔을때 일어나는 이벤트
    
    #region boolType
    public bool isDead = false; //오리가 죽었는지 체크 아 이거 public인거 불편한데 프로퍼티로 할 수도 없음
    private bool _isFalling = false; //오리가 떨어지고 있는 중인지 체크
    private bool _isMoving = true; //오리가 움직이고 있는 중인지 체크
    private bool _timeRunaway = false; //오리가 시간초과로 도망갈 경우
    private bool _runaway = false; //오리가 도망갈 경우
    private bool _soundPlay = false; //사운드가 재생 중인가?
    #endregion
    
    #region Value
    [Range(0, 20)]
    public float speed = 0.7f; //오리의 속도
    private Vector3 _sign = new Vector3(1, 1); //오리의 이동 방향
    private int _duckAngle = 0; //오리 sprite의 방향
    private float _time = 10f; //타이머를 위한 시계
    private float _acceleration = 0f; //가속도 stage에 따라 달라짐
    #endregion
    
    #region Component
    private Animator _duckAnim; //오리 애니메이션 컴포넌트를 담을 변수
    private Rigidbody _duckRigid; //오리 Rigidbody 컴포넌트를 담을 변수
    private AudioSource _duckAudio; //오리가 날아다니는 소리를 담고있는 오디오 소스 컴포넌트를 담을 변수
    #endregion
    
    #region String
    private const string RunAway = "Runaway"; //재생 할 애니메이션 string 미리 지정
    private const string Die = "Die";
    private const string DeadSound = "Duck_dead";
    #endregion
    
    #region Start, OnEnable
    private void Start()
    {
        _duckAnim = GetComponent<Animator>();
        _duckRigid = GetComponent<Rigidbody>();
        _duckAudio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //오리를 파괴 하는 것이 아닌 Active 제어를 통한 생성이므로 OnEnable사용
        _isMoving = true; //생성 되자마자 움직임
        _time = 10f; //시간 초기화
        _acceleration = GameManager.Instance.stage; //가속도 할당
    }
    #endregion
    
    private void Dead()
    {
        //오리 죽으면 생기는걸 유니티 이벤트로 구현하고 싶어서 onDuckDead 이벤트에 등록된 함수들을 모두 불러옴
        _isMoving = false; //죽었을때 움직이는 상태가 아님
        onDuckDead.Invoke();
        StartCoroutine(DuckDead()); //죽었을때 애니메이션 재생
    }
    
    public void Runaway()
    {
        //총알 세번 못맞추면 도망
        Debug.Log("오리야 도망가~(총알소진)");
        StartCoroutine(DuckRunAway());
    }

    IEnumerator DuckDead()
    {
        Debug.Log("오리가 죽었다");
        _duckAudio.Stop(); //오리가 날아다닐때 내는 소리는 내장 되어있기 때문에 일단 멈춤
        _soundPlay = false; //그리고 멈췄으니까 날아다닐 때 내는 소리도 꺼졌다라고 신호를 보내줌
        _duckAnim.SetBool(Die, true); //죽었을때 애니메ㅣ션 재생
        yield return new WaitForSeconds(1f); //잠시 멈춰 있는 시간
        _isFalling = true; //떨어지는 중
        AudioManager.instance.PlaySE(DeadSound);
        yield return new WaitForSeconds(3f);
        _isFalling = false; //떨어짐 멈춤
        gameObject.SetActive(false); //Active상태 false로 되돌림 => 리젠 가능
    }

    IEnumerator DuckRunAway()
    {
        //사운드 관련
        _duckAudio.Stop();
        _soundPlay = false;
        
        _isMoving = false; //움직이지 않음
        _runaway = true; //도망가는 상태
        _timeRunaway = false; //시간 초과에 의한 도망감은 아님
        
        onDuckRunaway.Invoke(); //onDuckRunaway 이벤트에 등록된 함수들을 모두 불러옴
        transform.rotation = Quaternion.Euler(0, 0, 0); //이게 문제네ㅋㅋㅋ
        _duckAnim.SetBool(RunAway, true); //도망갈때 애니메이션 재생
        yield return new WaitForSeconds(3f); //휴지
        gameObject.SetActive(false); //Active상태 false로 되돌림 => 리젠 가능
        //Runaway 이벤트 종료
        _runaway = false;
    }

    private void DuckMove()
    {
        if(!_isMoving) return; //움직이는 상태가 아니면 아래 코드를 실행 시키지 않고 되돌아감
        
        //벽면에 닿으면 꺾음
        if (transform.position.x <= -1.35f || transform.position.x >= 1.35f)
        {
            _sign.x *= -1;
            _duckAngle = _duckAngle == 0 ? 180 : 0;
            _duckRigid.rotation = Quaternion.Euler(0, _duckAngle, 0);
            Debug.Log("X" + _sign.x);
        }

        if (transform.position.y >= 1.33f || transform.position.y <= -0.35f)
        {
            _sign.y *= -1;
            Debug.Log("Y" + _sign.y);
        }

        if (!_soundPlay) _duckAudio.Play();
        _duckRigid.MovePosition(transform.position + _sign * Time.deltaTime * speed * _acceleration); //실제 움직임
        _soundPlay = true;
    }
    
    private void FixedUpdate()
    {
        //맞춤-죽음
        if (isDead)
        {
            Dead();
            isDead = false;
        }

        //죽었을때 떨어짐
        if (_isFalling)
        {
            _duckRigid.MovePosition(_duckRigid.position + Vector3.down * Time.deltaTime);
        }
        
        //10초뒤 도망, 총알 3번 못 맞추면 도망
        if (_runaway)
        {
            _duckRigid.MovePosition(_duckRigid.position + Vector3.up * Time.deltaTime);
        }
        
        //이동
        DuckMove();

        //타이머
        if (_isMoving)
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                Debug.Log("오리야 도망가~(시간초과)");
                _timeRunaway = true;
                _time = 10;
            }
            else GameManager.Instance.timeT.text = "Time \n" + Mathf.RoundToInt(_time);
        }

        //10초뒤 도망
        if (_timeRunaway)
        {
            StartCoroutine(DuckRunAway());
        }
    }
}
