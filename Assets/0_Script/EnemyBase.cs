using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class EnemyBase : CreatureBase
{
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (canAttack == false) return;
        if (isDie) return;

        MoveToPlayer();
        Debug.LogError("BEM ");
    }
    public bool canAttack;

    public void MoveToPlayer()
    {
        Debug.LogError("BEM 2233");

        if (_target == null) return;

        Debug.LogError("BEM 22");

        gameObject.transform.LookAt(_target.transform.position);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);


    }






    public GameObject _target;

    float basePosY;
    public GameObject posToMove;
    public Animator _anim;
    public float rdMove1, rdMove2, rdTime1, rdTime2;
    [Button]
    public void Wandering()
    {
        if (isDie) return;


        var rdX = Random.RandomRange(rdMove1, rdMove2);
        var rdZ = Random.RandomRange(rdMove1, rdMove2);
        var moveTime = Random.Range(rdTime1, rdTime2);
        Vector3 posToMove = new Vector3(rdX, transform.position.y, rdZ);

        transform.DOMove(posToMove, moveTime);
        FaceToPos(posToMove);
        ChangeState(EnemyState.Move);
    }

    public void FaceToPos(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        gameObject.transform.rotation = Quaternion.LookRotation(dir);
    }
    public GameObject font;
    [Button]
    public void Wandering2()
    {
        if (isDie) return;

        var delay = 0.5f;
        var rot = Random.RandomRange(0, 360);
        transform.DORotate(new Vector3(0, rot, 0), delay);

        Utils.ins.DelayCall(delay, () =>
        {
            transform.DOMove(font.transform.position, 2);
            ChangeState(EnemyState.Move);
        });


    }



    public bool isDie;
    public EnemyState _state;
    public void ChangeState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Move:
                _anim.SetBool("Move", true);
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.MoveToTarget:
                break;
            case EnemyState.Die:
                _anim.SetTrigger("Die");


                break;
        }



        _state = state;

    }


    [SerializeField] private float _curHP,_maxHP;

    public float curHP
    {
        get
        {
            return _curHP;
        }
        set
        {
            _curHP = value;
            UpdateHealthBar();
        }

    }

    public GameObject curHPBar;
    public void UpdateHealthBar()
    {
        if (curHP <= 0)
        {

            curHPBar.transform.localScale = new Vector3(0, 0.5f, 1);

            ChangeState(EnemyState.Die);
            return;
        }

        var scale = -100f;
        if (curHP != 0) scale = _curHP / _maxHP;

        curHPBar.transform.localScale = new Vector3(scale, 0.5f, 1);
    }
    public void TakeDamage()
    {
        if (curHP <= 0)
        {
            isDie = true;
            curHPBar.transform.localScale = new Vector3(0, 0.5f, 1);

            ChangeState(EnemyState.Die);
            return;
        }

        if (isDie) return;
        Debug.LogError("IS DIE" + isDie + "TAKE DAMAGE");
        _anim.SetTrigger("TakeDamage");
        curHP--;
    }



    public enum EnemyState
    {
        Move,
        Attack,
        MoveToTarget,
        Die,


    }
}