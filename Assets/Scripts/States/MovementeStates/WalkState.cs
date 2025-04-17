using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState<MovementStateType>
{
    [SerializeField] int speed;
    [SerializeField] float drag;
    MovementHandler movementHandler;
    MovementStateType type;
    public WalkState(MovementStateType newType, MovementHandler newMovementHandler, int newSpeed, float newDrag)
    {
        type = newType;
        movementHandler = newMovementHandler;
        speed = newSpeed;
        drag = newDrag;
    }

    public override MovementStateType Type { get => type; }



    public override void OnEnter(BaseStateMachine<MovementStateType> baseStateMachine)
    {
        movementHandler.Rigid.mass = 0.8f;
        movementHandler.Rigid.linearDamping = drag;
    }

    public override void OnExit(BaseStateMachine<MovementStateType> baseStateMachine)
    {

    }

    public override void OnFixedUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {
        throw new System.NotImplementedException();
    }

    public override void OnLateUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {
        movementHandler.HandleMovement(speed);
    }
}
