using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Duck : MonoBehaviour
{
    //유니티 이벤트로 Duck 죽었을때 나오는거 구현
    public UnityEvent onDuckDead;
    #region boolType
    //오리의 죽음 체크 일단 테스트용으로 public 선언 해놓음
    public bool isDead = false;
    private bool _isFalling = false;
    private bool _isMoving = true;
    private bool _timeRunaway = false;
    private bool _runaway = false;
    #endregion
    #region Value
    [Range(0, 20)]
    public float speed = 2f;
    private int _signX = 1;
    private int _signY = 1;
    private int _duckAngle = 0;
    private float _time = 10f;
    private float _acceleration = 0f;
    #endregion
    #region Component
    private Animator _duckAnim;
    private Rigidbody2D _duckRigid;
    #endregion
    #region String
    private const string RunAway = "Runaway";
    private const string Die = "Die";
    #endregion


    #region Start, OnEnable
    private void Start()
    {
        _duckAnim = GetComponent<Animator>();
        _duckRigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _isMoving = true;
        _time = 10f;
        _acceleration = GameManager.Instance.stage;
    }
    #endregion


    private void Dead()
    {
        //오리 죽으면 생기는걸 유니티 이벤트로 구현하고 싶어서 onDuckDead 이벤트에 등록된 함수들을 모두 불러옴
        _isMoving = false;
        onDuckDead.Invoke();
        StartCoroutine(DuckDead());
    }

    IEnumerator DuckDead()
    {
        Debug.Log("오리가 죽었다");
        _duckAnim.SetBool(Die, true);
        yield return new WaitForSeconds(1f);
        _isFalling = true;
        yield return new WaitForSeconds(3f);
        _isFalling = false;
        gameObject.SetActive(false);
    }

    IEnumerator DuckRunAway()
    {
        //위로 날아감
        _isMoving = false;
        _runaway = true;
        _timeRunaway = false;
        //위로 날아감
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _duckAnim.SetBool(RunAway, true);
        UIManager.Instance.Damaged();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void DuckMove()
    {
        if(!_isMoving) return;
        
        //벽면에 닿으면 꺾음
        if (transform.position.x <= -1.35f || transform.position.x >= 1.35f)
        {
            _signX *= -1;
            _duckAngle = _duckAngle == 0 ? 180 : 0;
            transform.rotation = Quaternion.Euler(0, _duckAngle, 0);
            Debug.Log("X" + _signX);
        }

        if (transform.position.y >= 1.33f || transform.position.y <= -0.35f)
        {
            _signY *= -1;
            Debug.Log("Y" + _signY);
        }

        Vector2 vec = new Vector2(_signX, _signY);
        _duckRigid.MovePosition((Vector2)transform.position + vec * Time.deltaTime * speed * _acceleration);
    }

    public void Runaway()
    {
        //총알 세번 못맞추면 도망
        Debug.Log("오리야 도망가~(총알소진)");
        StartCoroutine(DuckRunAway());
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
            _duckRigid.MovePosition(_duckRigid.position + Vector2.down * Time.deltaTime);
        }
        
        //10초뒤 도망, 총알 3번 못 맞추면 도망
        if (_runaway)
        {
            _duckRigid.MovePosition(_duckRigid.position + Vector2.up * Time.deltaTime);
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
            else UIManager.Instance.timeT.text = "Time \n" + Mathf.RoundToInt(_time);
        }

        //10초뒤 도망
        if (_timeRunaway)
        {
            StartCoroutine(DuckRunAway());
        }
    }
}
