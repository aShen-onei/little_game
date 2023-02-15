using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    // walk speed
    public float Speed = 2f;
    public MoveManager Move;
    public Camera MainCamera;
    public Transform MainCaremaTransForm;
    public Transform Charater;
    public float CursorPlaneHeight = 0.5f;
    /// <summary>
    /// private var
    /// </summary>
    private Vector3 InitOffsetToPlayer;
    private Vector3 CursorScreenPosition;
    private Plane PlayerMovementPlane;
    private Quaternion ScreenMovementSpace;
    private Vector3 ScreenMovementForward;
    private Vector3 ScreenMovementRight;

    private void Awake()
    {
        Move.MoveDirection = Vector2.zero;
        Move.FacingDirection = Vector2.zero;

        // set carema
        MainCamera = Camera.main;
        MainCaremaTransForm = MainCamera.transform;

        if (!Charater) Charater = transform;
        InitOffsetToPlayer = MainCaremaTransForm.position - Charater.position;
        CursorScreenPosition = new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0f);
        PlayerMovementPlane = new Plane(Charater.up, Charater.position + Charater.up * CursorPlaneHeight);
    }

    private void Start()
    {
        ScreenMovementSpace = Quaternion.Euler(0, MainCaremaTransForm.eulerAngles.y, 0);
        ScreenMovementForward = ScreenMovementSpace * Vector3.forward;
        ScreenMovementRight = ScreenMovementSpace * Vector3.right;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateCarema();
    }
    void UpdateCarema()
    {
        Vector3 caremaTargetPosiiton = Charater.position + InitOffsetToPlayer;
        MainCaremaTransForm.position = caremaTargetPosiiton;
    }

    void UpdateMovement()
    {
        Vector3 direction = Input.GetAxis("Horizontal") * ScreenMovementRight +
                            Input.GetAxis("Vertical") * ScreenMovementForward;
        Move.MoveDirection = direction.normalized;
        UpdateFacingDirection();
    }

    void UpdateFacingDirection()
    {
        PlayerMovementPlane.normal = Charater.up;
        PlayerMovementPlane.distance = -Charater.position.y + CursorPlaneHeight;
        Vector3 CurPosition = Input.mousePosition;
        Vector3 CurWorldPosition = ScreenToWorldPosition(CurPosition, PlayerMovementPlane, MainCamera);

        Move.FacingDirection = (CurWorldPosition - Charater.position);
        Move.FacingDirection.y = 0f;
    }

    public static Vector3 ScreenToWorldPosition(Vector3 ScreenPostiton, Plane MovePlane, Camera carema)
    {
        Ray ray = carema.ScreenPointToRay(ScreenPostiton);
        return PlaneRayIntersection(MovePlane, ray);

    }
    public static Vector3 PlaneRayIntersection(Plane plane, Ray ray)
    {
        float dist = 0f;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }
}
