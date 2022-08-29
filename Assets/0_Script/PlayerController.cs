using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerController : CreatureBase
{



    public PetController pet;

    private void FixedUpdate()
    {
        MoveByJoyStick();
        ListenInput();
    }

    public VariableJoystick _joystick;
    public Rigidbody _rigid;
    public bool canMove;
    public float speedMove;
    public Animator _anim;
    public characterStateEnum _playerState;
    public void MoveByJoyStick()
    {



        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
        Vector3 dir = _joystick.Direction;
        if (dir != Vector3.zero)
        {
            ChangeState(characterStateEnum.move);
            dir.z = dir.y;
            dir.y = 0;
            dir = dir * speedMove * Time.fixedDeltaTime;
            gameObject.transform.position += dir;

            gameObject.transform.rotation = Quaternion.LookRotation(dir);
            Debug.LogError(33);

        }
        else
        {
            ChangeState(characterStateEnum.idle);
        }

    }



    public void ListenInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FireBulletIntervals();
        }

        if (Input.GetKey(KeyCode.S))
        {
            ChangeState(characterStateEnum.attackRange);
        }


    }


    [Button]
    public void ChangeState(characterStateEnum state, GameObject target = null)
    {
        switch (state)
        {

            case characterStateEnum.move:
                _anim.SetBool("isMoving", true);

                break;
            case characterStateEnum.idle:
                _anim.SetBool("isMoving", false);

                break;
            case characterStateEnum.attackRange:
                _targetRange = target;
                _anim.SetBool("hasTarget", true);
                AttackRange();


                break;
            case characterStateEnum.die:
                break;
            case characterStateEnum.fly:
                break;

            case characterStateEnum.attackMelee:
                _targetMelee = target;
                Debug.LogError(target);
                AttackMelee();

                break;
        }

        _playerState = state;




    }


    public GameObject sword, gun, _targetMelee, _targetRange;
    public bool isAttackingMelee;
    public override void AttackMelee()
    {
        if (_targetMelee == null) return;
        _anim.SetTrigger("attackMelee");
        isAttackingMelee = true;
        sword.gameObject.SetActive(true);
        gun.gameObject.SetActive(false);

    }

    public void FinishAttackMeleeAnimEvent()
    {
        AttackMelee();
    }

    public void TargetOutRangeMelee()
    {
        _targetMelee = null;
        isAttackingMelee = false;

    }
    public void FaceToTarget(GameObject target)
    {
        var pos = target.transform.position;
        pos.y = 0;
        transform.LookAt(pos);
    }





    public GameObject yellowMuzzle, yellowBullet, yellowImpact, muzzlePos;
    public override void AttackRange()
    {
        if (isAttackingMelee == true) return;
        if (_targetRange.transform.parent.GetComponent<EnemyBase>().isDie)
        {
            _anim.SetBool("attackRange", false);
            _anim.SetBool("hasTarget", false);
            return;
        }


        FaceToTarget(_targetRange);
        gun.gameObject.SetActive(true);
        sword.gameObject.SetActive(false);
        _anim.Play("meleeLayer_NoAnim");
        _anim.SetBool("attackRange", true);
        Debug.LogError("ATTACK Range");



    }


    public void FireBulletIntervals()
    {
        if (isAttackingMelee == true) return;
        if (_targetRange == null) return;
        if (_targetRange.transform.parent.GetComponent<EnemyBase>().isDie) return;


        var newBullet = Instantiate(yellowBullet);
        newBullet.transform.position = muzzlePos.transform.position;

        var posLook = _targetRange.transform.position;
        posLook.y = 0;
        newBullet.transform.LookAt(_targetRange.transform.parent.position);


        var newMuzzle = Instantiate(yellowMuzzle);
        newMuzzle.transform.position = newBullet.transform.position;
        newMuzzle.transform.rotation = newBullet.transform.rotation;





    }
    public void FinishAttackRange()
    {
        ChangeState(characterStateEnum.attackRange);

    }






}
public enum characterStateEnum
{
    move,
    idle,
    attackRange,
    die,
    fly,
    attackMelee
}