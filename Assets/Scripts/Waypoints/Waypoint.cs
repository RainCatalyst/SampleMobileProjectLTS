using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EventChannels;

public class Waypoint : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] protected GameEventChannelSO gameEvents;

    [Header("Navigation")]
    [SerializeField] Waypoint nextWaypoint;
    [SerializeField] Transform playerPoint;

    public bool IsActive => active;
    public bool IsCompleted => completed;
    public Waypoint NextWaypoint => nextWaypoint;
    public Transform PlayerPoint => playerPoint;

    bool active;
    bool completed;

    protected virtual void Start() {}

    public virtual void Complete()
    {
        // Immediate waypoint completion
        gameEvents.CompleteWaypoint(this);
        completed = true;
    }

    public virtual void Enter()
    {
        // Enable enemies, play effects
        gameEvents.EnterWaypoint(this);
        active = true;
    }

    public virtual void Exit()
    {
        // Leaving for the next waypoint
        gameEvents.ExitWaypoint(this);
        active = false;
    }
}
