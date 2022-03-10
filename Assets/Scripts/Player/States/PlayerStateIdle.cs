using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

public class PlayerStateIdle : BaseState
{
    PlayerController player;
    bool pressedMove;

    public PlayerStateIdle(StateMachine stateMachine, PlayerController player) : base("Idle", stateMachine)
    {
        this.player = player;
    }

    public override void Enter(BaseState fromState)
    {
        base.Enter(fromState);
        // Extra logic when entering an idle state
    }

    public override void Exit()
    {
        base.Exit();
        // Extra logic when exiting an idle state
    }

    public override void InputUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            pressedMove = true;
    }

    public override void Update()
    {
        if (pressedMove) {
            player.currentWaypoint?.Complete();
            pressedMove = false;
        }
    }
}
