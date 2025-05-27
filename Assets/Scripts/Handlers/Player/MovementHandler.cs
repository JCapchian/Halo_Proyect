using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class MovementHandler : BaseStateMachine<MovementStateType>
{
    [Header("Managers")]
    [SerializeField] InputManager inputManager;


    [Header("Components")]
    [SerializeField] Transform orientation;
    [SerializeField] Rigidbody rigid;
    public Rigidbody Rigid { get { return rigid; } private set { } }

    Vector2 axis;
    public Vector2 Axis { get { return axis; } private set { } }

    [Header("Stats")]
    [SerializeField] int walkSpeed;
    [SerializeField] int runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundDistance;
    [SerializeField] float groundDrag;
    [SerializeField] float airDrag;
    [SerializeField] LayerMask groundMask;

    [Header("States")]
    [SerializeField] bool isGrounded;
    [SerializeField] float currentDrag;

    public void Initialize(PlayerController playerController)
    {
        inputManager = playerController.InputManager;

        playerController.InputManager.onMovement += GetAxis;

        LoadStates();
        rigid.linearDamping = groundDrag;
    }

    protected override void LoadStates()
    {
        dictionaryStates = new Dictionary<MovementStateType, BaseState<MovementStateType>>();

        dictionaryStates.Add(MovementStateType.Idle, new IdleState(MovementStateType.Idle, this));
        dictionaryStates.Add(MovementStateType.Walk, new WalkState(MovementStateType.Walk, this, walkSpeed, groundDrag));
        dictionaryStates.Add(MovementStateType.Air, new AirState(MovementStateType.Air, this, groundDrag, airDrag));
        dictionaryStates.Add(MovementStateType.Run, new RunState(MovementStateType.Run, runSpeed));

        currentState = dictionaryStates[MovementStateType.Idle];
    }

    void GetAxis(Vector2 _axis)
    {
        axis = _axis;
    }

    public void HandleMovement(int movementSpeed)
    {
        currentDrag = rigid.linearDamping;
        Vector3 moveForce = ((transform.right * axis.x) + (transform.forward * axis.y)).normalized
                            * movementSpeed
                            * 10f;
        rigid.AddForce(moveForce.normalized * movementSpeed * 10f, ForceMode.Force);
    }

    public void GroundChecker()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, 0.3f + groundDistance, groundMask);

        if (!isGrounded) SwitchState(MovementStateType.Air);
        if (currentState == dictionaryStates[MovementStateType.Air])
            if (isGrounded)
                SwitchState(MovementStateType.Idle);
    }

    // public void Jump()
    // {
    //     if (!isGrounded)
    //         return;

    //     SwitchState("Jump");
    //     Vector3 jumpDirection = transform.up * jumpForce * 5f;
    //     rigid.AddForce(jumpDirection, ForceMode.Impulse);
    // }

    // public void ChangeDrag(float newDrag)
    // {
    //     rigid.drag = newDrag;
    // }
    // public void ChangeMass(float newMass)
    // {
    //     rigid.mass = newMass;
    // }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(orientation.position * 0.2f, Vector3.down);
    }
}
