using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState : BaseState<MovementStateType>
{
    MovementHandler movementHandler;
    MovementStateType type;
    public StopState(MovementStateType newType, MovementHandler newMovementHandler)
    {
        type = newType;
        movementHandler = newMovementHandler;
    }

    public override MovementStateType Type { get => type; }
    public StopState(MovementStateType newType)
    {
        this.type = newType;
    }

    public override void OnEnter(BaseStateMachine<MovementStateType> baseStateMachine)
    {

    }

    public override void OnExit(BaseStateMachine<MovementStateType> baseStateMachine)
    {

    }

    public override void OnFixedUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {
        movementHandler.HandleMovement(0);
    }

    public override void OnLateUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {

    }

    public override void OnUpdate(BaseStateMachine<MovementStateType> baseStateMachine)
    {
        // if (movementHandler.Axis.magnitude == 1)
        //     movementHandler.SwitchState(MovementStateType.Walk);
    }
}
