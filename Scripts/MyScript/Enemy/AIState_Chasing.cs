using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ¿¿½ü×·ÖðÍæ¼Ò×´Ì¬
/// </summary>
public partial class EnemyAI : StateMachine
{
    public void ChasingStart()
    {
        MoveTo(_newTargetPosition);
    }

    public void ChasingUpdate()
    {
        if(!scanner.playerInSight && moveController.IsStopped())
        {
            SetState("AIState_Waiting");
            return;
        }
        if(_inShootingRange && scanner.playerInSight)
        {
            SetState("AIState_Shooting");
            return;
        }
        if(_newTargetPosition != _lastTargetPosition)
        {
            _lastTargetPosition = _newTargetPosition;
            MoveTo(_newTargetPosition);
            return;
        }
    }

    public void ChasingExit()
    {

    }
}