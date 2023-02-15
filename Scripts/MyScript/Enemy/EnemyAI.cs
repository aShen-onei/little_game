using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人AI状态机，分类，这个文件写主要状态控制
/// </summary>
public partial class EnemyAI : StateMachine
{
    public MoveController moveController;
    public Scanner scanner;
    public Rigidbody _rigidbody;
    public SignalSender signalFire;
    public SignalSender signalStopFire;
    public Transform[] patrolPoints;


    private GameObject player;
    private Vector3 _lastTargetPosition;
    private Vector3 _newTargetPosition;
    private bool _inShootingRange = false;

    public override void Start()
    {
        player = GameObject.Find("Player");


        AddState("AIState_Patrolling", PatrollingUpdate, PatrollingStart, PatrollingExit);
        AddState("AIState_Chasing", ChasingUpdate, ChasingStart, ChasingExit);
        AddState("AIState_Shooting", ShootingUpdate, ShootingStart, ShootingExit);
        AddState("AIState_Waiting", WaitingUpdate, WaitingStart, WaitingExit);

        SetState("AIState_Patrolling");

    }
    public void OnEnable()
    {
        if(!IsEmpty())
        {
            SetState("AIState_Patrolling");
        }
    }
    public void MoveTo(Vector3 targetPos)
    {
        moveController.MoveTo(targetPos);
    }
    public void LostTarget()
    {

    }
    public void GetTarget(Vector3 targetPos)
    {
        targetPos.y = 0f;
        _newTargetPosition = targetPos;
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            _inShootingRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            _inShootingRange = false;
        }
    }
}
