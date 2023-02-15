using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 等待状态
/// </summary>
public partial class EnemyAI : StateMachine
{
    public float waitingTime = 3f;
    private float waitingTimer = 0f;

    /// <summary>
    /// 进入等待的函数钩子
    /// </summary>
    public void WaitingStart()
    {
        waitingTimer = 0f;
    }
    /// <summary>
    /// 状态更新
    /// </summary>
    public void WaitingUpdate()
    {
        if(scanner.playerInSight)
        {
            SetState("AIState_Chasing");
            return;
        }
        waitingTimer += Time.deltaTime;
        if(waitingTimer >= waitingTime)
        {
            SetState("AIState_Patrolling");
        }
    }

    public void WaitingExit()
    {

    }
}
