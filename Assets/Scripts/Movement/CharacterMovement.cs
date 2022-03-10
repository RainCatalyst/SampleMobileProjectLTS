using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] float rotationAlignSpeed = 3f;
    [SerializeField] float stoppingDistance = 0.25f;

    [Header("Animation Properties")]
    [SerializeField] Animator animator;

    NavMeshAgent agent;
    Transform target;
    Quaternion targetRotation;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        bool moving = IsMoving();

        if (!moving) {
            // Align rotation when arriving to waypoint
            agent.updateRotation = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationAlignSpeed);
        }else{
            // Let NavMesh update the rotation while moving
            agent.updateRotation = true;
        }

        // Update animation based on velocity
        animator.SetFloat("Move", agent.velocity.magnitude / agent.speed);
    }

    public void MoveTo(Transform target)
    {
        this.target = target;
        targetRotation = Quaternion.LookRotation(target.forward, Vector3.up);
        agent.SetDestination(target.position);
    }

    public bool IsMoving() => agent.pathPending || agent.remainingDistance > stoppingDistance;
}
