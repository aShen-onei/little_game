using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [HideInInspector]
    public Vector3 MoveDirection;
    [HideInInspector]
    public Vector3 MoveTarget;
    [HideInInspector]
    public Vector3 FacingDirection;

    public float WalkSpeed = 5.0f;
    public float MoveSnappyness = 50f;
    public float TuringSMA = 0.3f;

    void FixedUpdate()
    {
        var targetSpeed = MoveDirection * WalkSpeed;
        var rigbody = GetComponent<Rigidbody>();
        var deltaSpeed = targetSpeed - rigbody.velocity;
        if (rigbody.useGravity) deltaSpeed.y = 0;
        rigbody.AddForce(deltaSpeed * MoveSnappyness, ForceMode.Acceleration);
        UpdateRoation();
    }

    void UpdateRoation()
    {
        // setup the face dir
        var faceDir = FacingDirection;
        var rigbody = GetComponent<Rigidbody>();
        // if face direction is 0, that means forward
        if (faceDir == Vector3.zero) faceDir = MoveDirection;
        // rigibody.angularVel jiao su du
        if(faceDir == Vector3.zero)
        {
            rigbody.angularVelocity = Vector3.zero;
        } else
        {
            var angle = GetRoationAngle(transform.forward ,faceDir, Vector3.up);
            rigbody.angularVelocity = Vector3.up * angle * TuringSMA;
        }
    }
    /// <summary>
    /// get roation angle
    /// </summary>
    /// <param name="dirA">origin dir</param>
    /// <param name="dirB">target dir</param>
    /// <param name="axis">roate axis</param>
    /// <returns></returns>
    float GetRoationAngle(Vector3 dirA, Vector3 dirB, Vector3 axis)
    {
        // touying dao axis 
        dirA = dirA - Vector3.Project(dirA, axis);
        dirB = dirB - Vector3.Project(dirB, axis);

        float angle = Vector3.Angle(dirA, dirB);
        return angle * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
    }
}
