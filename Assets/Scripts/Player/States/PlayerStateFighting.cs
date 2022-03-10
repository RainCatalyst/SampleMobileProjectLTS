using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

public class PlayerStateFighting : BaseState
{
    PlayerController player;
    bool pressedShoot;

    public PlayerStateFighting(StateMachine stateMachine, PlayerController player) : base("Fighting", stateMachine)
    {
        this.player = player;
    }

    public override void Enter(BaseState fromState)
    {
        base.Enter(fromState);
        // Extra logic when entering a fight
    }

    public override void Exit()
    {
        base.Exit();
        // Extra logic when exiting a fight
    }

    public override void InputUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            pressedShoot = true;
    }

    public override void Update()
    {
        if (pressedShoot) {
            player.shooting.Shoot();
            pressedShoot = false;
        }
    }
}
