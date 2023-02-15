using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AI�ű���ɨ���������Ƿ���player
/// 
/// </summary>
public class Scanner : MonoBehaviour
{
    // ��Ұ��Χ
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
        // �����½Ƕ�
        float angle = Vector3.Angle(direction, transform.forward);
        // ��������
        if(angle < viewAngle * 0.5f)
        {
            RaycastHit hit;
            // ����������ҷ������м��Ƿ����ڵ���
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
