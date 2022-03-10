using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

public class PlayerStateMoving : BaseState
{
    public Waypoint destinationWaypoint;

    PlayerController player;
    // bool pressedShoot;

    public PlayerStateMoving(StateMachine stateMachine, PlayerController player) : base("Moving", stateMachine)
    {
        this.player = player;
    }

    public override void Enter(BaseState fromState)
    {
        base.Enter(fromState);
        // Exit current waypoint (if any)
        player.currentWaypoint?.Exit();
        // Move towards the destination
        player.movement.MoveTo(destinationWaypoint.PlayerPoint);
    }

    public override void Exit()
    {
        base.Exit();
        // Enter the destination waypoint
        destinationWaypoint.Enter();
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
        // if (Input.GetMouseButtonDown(0))
        //     pressedShoot = true;
    }

    public override void Update()
    {
        // TODO: Use events from player.movement
        if (!player.movement.IsMoving())
            EnterWaypoint();

        // Allow player to shoot while moving?

        // if (pressedShoot)
        // {
        //     player.shooting.Shoot();
        //     pressedShoot = false;
        // }
    }

    void EnterWaypoint()
    {
        if (destinationWaypoint is EnemyWaypoint)
            stateMachine.ChangeState(player.stateFighting);
        else
            stateMachine.ChangeState(player.stateIdle);
    }

    public void UpdateTarget(Waypoint waypoint)
    {
        destinationWaypoint = waypoint;
        player.movement.MoveTo(destinationWaypoint.PlayerPoint);
    }
}
