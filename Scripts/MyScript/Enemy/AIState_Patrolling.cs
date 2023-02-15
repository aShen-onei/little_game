using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ñ²Âß×´Ì¬
/// </summary>
public partial class EnemyAI : StateMachine
{
    private int wayPointIndex = 0;
    public void PatrollingStart()
    {
        wayPointIndex++;
        if(wayPointIndex >= patrolPoints.Length) wayPointIndex = 0;
    }

    public void PatrollingUpdate()
    {
        if(scanner.playerInSight)
        {
            SetState("AIState_Chasing");
            return;
        }
        if(moveController.IsStopped())
        {
            SetState("AIState_Waiting");
            return;
        }
    }

    public void PatrollingExit()
    {

    }
}

