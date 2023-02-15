using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ȴ�״̬
/// </summary>
public partial class EnemyAI : StateMachine
{
    public float waitingTime = 3f;
    private float waitingTimer = 0f;

    /// <summary>
    /// ����ȴ��ĺ�������
    /// </summary>
    public void WaitingStart()
    {
        waitingTimer = 0f;
    }
    /// <summary>
    /// ״̬����
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
