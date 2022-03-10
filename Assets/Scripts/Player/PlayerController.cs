using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;
using EventChannels;

public class PlayerController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public GameEventChannelSO gameEvents;

    #region states
    StateMachine stateMachine;
    [HideInInspector] public PlayerStateIdle stateIdle;
    [HideInInspector] public PlayerStateMoving stateMoving;
    [HideInInspector] public PlayerStateFighting stateFighting;
    #endregion
    [HideInInspector] public CharacterMovement movement;
    [HideInInspector] public PlayerShooting shooting;
    [HideInInspector] public Waypoint nextWaypoint, currentWaypoint;

    void OnEnable() {
        gameEvents.OnWaypointSet += SetWaypoint;
        gameEvents.OnWaypointCompleted += OnWaypointCompleted;
        gameEvents.OnWaypointEntered += OnWaypointEntered;
        gameEvents.OnWaypointExited += OnWaypointExited;
    }

    void OnDisable()
    {
        gameEvents.OnWaypointSet -= SetWaypoint;
        gameEvents.OnWaypointCompleted -= OnWaypointCompleted;
        gameEvents.OnWaypointEntered -= OnWaypointEntered;
        gameEvents.OnWaypointEntered -= OnWaypointExited;
    }

    void Awake()
    {
        stateMachine = new StateMachine();
        stateIdle = new PlayerStateIdle(stateMachine, this);
        stateMoving = new PlayerStateMoving(stateMachine, this);
        stateFighting = new PlayerStateFighting(stateMachine, this);

        stateMachine.Initialize(stateIdle);
    }

    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        shooting = GetComponent<PlayerShooting>();
    }

    void Update()
    {
        stateMachine.InputUpdate();
        stateMachine.Update();
    }

    void MoveToNextWaypoint()
    {
        if (nextWaypoint != null)
            gameEvents.SetWaypoint(nextWaypoint);
    }

    void SetWaypoint(Waypoint waypoint)
    {
        stateMoving.destinationWaypoint = waypoint;
        if (stateMachine.CurrentState == stateMoving)
            stateMoving.UpdateTarget(waypoint);
        stateMachine.ChangeState(stateMoving);
    }

    void OnWaypointCompleted(Waypoint waypoint)
    {
        // Ignore if not current waypoint
        if (currentWaypoint != null && waypoint != currentWaypoint)
            return;
        
        // Find the next uncompleted waypoint
        var next = waypoint.NextWaypoint;
        while (next != null && next.IsCompleted)
        {
            if (next.NextWaypoint == null)
                break;
            next = next.NextWaypoint;
        }
        nextWaypoint = next;
        MoveToNextWaypoint();
    }

    void OnWaypointEntered(Waypoint waypoint)
    {
        currentWaypoint = waypoint;
    }

    void OnWaypointExited(Waypoint waypoint)
    {
        currentWaypoint = null;
    }
}
