using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 射击状态
/// </summary>
public partial class EnemyAI : StateMachine
{
    IEnumerator FreeFire()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            signalFire.SendSignals(this);
            yield return new WaitForSeconds(0.2f);
            signalStopFire.SendSignals(this);
        }
    }
    public void ShootingStart()
    {
        moveController.Stop();
        // 启动一个协程
        StartCoroutine("FreeFire");
    }

    public void ShootingUpdate()
    {
        if(!scanner.playerInSight)
        {
            SetState("AIState_Chasing");
            return;
        }
        AimAt(_newTargetPosition);
    }

    public void ShootingExit()
    {
        signalStopFire.SendSignals(this);
        // 把当前的这个停掉
        StopCoroutine("FreeFire");
    }

    public void AimAt(Vector3 position)
    {
        _rigidbody.transform.LookAt(position);
    }
    
}
