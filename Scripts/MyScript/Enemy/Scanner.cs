using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AI脚本，扫描视线内是否有player
/// 
/// </summary>
public class Scanner : MonoBehaviour
{
    // 视野范围
    public float viewAngle = 110f;
    public struct x
    {
        int s;
    }
    public x i;
    public SignalSender playerInSightSignal = new SignalSender();
    public SignalSender playerOutSightSignal = new SignalSender();

    [HideInInspector]
    public bool playerInSight;

    private SphereCollider _col;
    private GameObject _player;
    private Vector3 _lastPlayerPosition;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        playerInSight = false;
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject == _player)
        {
            if(DetectPlayer())
            {
                playerInSightSignal.SendSignals(this, _lastPlayerPosition);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            if (!DetectPlayer())
            {
                playerOutSightSignal.SendSignals(this);
            }
        }
    }
    public bool DetectPlayer()
    {
        playerInSight = false;

        Vector3 direction = _player.transform.position - transform.position;
        // 计算下角度
        float angle = Vector3.Angle(direction, transform.forward);
        // 在视线内
        if(angle < viewAngle * 0.5f)
        {
            RaycastHit hit;
            // 视线内往玩家方向检查中间是否有遮挡物
            bool isHit = Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, _col.radius);
            if(isHit)
            {
                if(hit.collider.gameObject == _player)
                {
                    playerInSight = true;
                    _lastPlayerPosition = _player.transform.position;
                }
            }
        }
        return playerInSight;
    }
}
