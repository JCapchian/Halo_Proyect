using System;
using UnityEngine;

public class AirState : BaseState<MovementStateType>
{
    float groundDrag;
    float airDrag;
    MovementHandler movementHandler;
    MovementStateType type;
    public AirState(MovementStateType newType, MovementHandler newMovementHandler, float nweGroundDrag, float newAirDrag)
    {
        movementHandler = newMovementHandler;
        type = newType;
        groundDrag = nweGroundDrag;
        airDrag = newAirDrag;
    }

    public override MovementStateType Type { get => type; }

    public override void OnEnter(BaseStateMachine<MovementStateType> baseStateMachine)
    {
        movementHandler.Rigid.linearDamping = airDrag;
    }

    public override void OnExit(BaseStateMachine<MovementStateType> baseStateMachine)
    {
        movementHandler.Rigid.mass = 2;
        movementHandler.Rigid.linearDamping = groundDrag;
    }

    public override void OnFixedUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {

    }

    public override void OnLateUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {

    }

    public override void OnUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {

    }
}
