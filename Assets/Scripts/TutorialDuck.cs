using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDuck : MonoBehaviour
{
    //상속으로 구현하기   
    #region boolType
    public bool isDead = false; //오리가 죽었는지 체크 아 이거 public인거 불편한데 프로퍼티로 할 수도 없음
    private bool _isFalling = false; //오리가 떨어지고 있는 중인지 체크
    private bool _isMoving = true; //오리가 움직이고 있는 중인지 체크
    private bool _soundPlay = false; //사운드가 재생 중인가?
    #endregion
    
    #region Value
    [Range(0, 20)]
    public float speed = 0.5f;
    private Vector3 _sign = new Vector3(1, 1);
    private int _duckAngle = 0;
    #endregion
    
    #region Component
    private Animator _duckAnim;
    private Rigidbody _duckRigid;
    private AudioSource _duckAudio;
    #endregion
    
    #region String
    private const string RunAway = "Runaway";
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
        _isMoving = true;
        DialogueManager.Instance.dialogueIdx++;
        DialogueManager.Instance.gameObject.SetActive(false);
    }
    #endregion

    public void Dead()
    {
        _isMoving = false;
        StartCoroutine(DuckDead());
    }
     
    IEnumerator DuckDead()
    {
        Debug.Log("오리가 죽었다");
        _duckAudio.Stop();
        _soundPlay = false;
        _duckAnim.SetBool(Die, true);
        yield return new WaitForSeconds(1f);
        _isFalling = true;
        AudioManager.instance.playSE(DeadSound);
        yield return new WaitForSeconds(3f);
        DialogueManager.Instance.eventProgress = false;
        _isFalling = false;
        DialogueManager.Instance.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void DuckMove()
    {
        if(!_isMoving) return;
        
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
        _duckRigid.MovePosition(transform.position + _sign * Time.deltaTime * speed);
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

        //이동
        DuckMove();
    }
}
