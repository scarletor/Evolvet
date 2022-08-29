using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{

    public PlayerController parent;
    public PetController parentPet;
    public void FinishAttackMeleePlayer()
    {
        parent.FinishAttackMeleeAnimEvent();
    }

    public void FinishAttackRangePlayer()
    {
        parent.FinishAttackRange();
    }

    public void StartAttackRangeAnim()
    {
        parent.FireBulletIntervals();
    }


    public void FinishAttackPet()
    {
        parentPet.FinishAttackPet();
    }



    public EnemyBase enemy;
    public void EnemyBatLordTakeDamage()
    {
        enemy.ChangeState(EnemyBase.EnemyState.Move);
    }

}
