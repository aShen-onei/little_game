using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// AI�ƶ�
/// </summary>
public class MoveController : MonoBehaviour
{
    /// <summary>
    /// ʹ��unity�Դ��ĵ��������㷨
    /// </summary>
    private NavMeshAgent nav;
    public void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector3 position)
    {
        nav.SetDestination(position);
        nav.isStopped = false;
    }
    public void Stop()
    {
        nav.isStopped = true;
    }

    public bool IsStopped()
    {
        return nav.isStopped;
        // return nav.remainingDistance <= nav.stoppingDistance;
    }
}
