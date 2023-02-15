using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Anime : MonoBehaviour
{
    // play animtor
    public Animator PlayerAnimator;
    private float Speed;
    private float Angle;
    private Transform Tr;
    // the last move position;
    private Vector3 LastPos;
    private Vector3 LocalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // for the init, all the same data;
        Tr = transform;
        LastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // the world current speed
        Vector3 vel = GetComponent<Rigidbody>().velocity; //(Tr.position - LastPos) / Time.deltaTime;
        
        // change the world speed to the local speed;
        LocalSpeed = Tr.InverseTransformDirection(vel);
        // no vertical speed;
        LocalSpeed.y = 0;
        Speed = LocalSpeed.magnitude;
        Angle = (AngleAlg(LocalSpeed) + 360f) % 360f;
        LastPos = Tr.position;
        PlayerAnimator.SetFloat("Speed", Speed);
        PlayerAnimator.SetFloat("Angle", Angle);
    }
    float AngleAlg(Vector3 direction)
    {
        return Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
    }
}
