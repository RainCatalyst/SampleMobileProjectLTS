using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRagdoll : MonoBehaviour
{
    [SerializeField] Animator character;

    public bool IsRagdoll => ragdoll;

    bool ragdoll;
    Collider mainCollider;

    void Awake() {
        mainCollider = GetComponent<Collider>();
    }

    void Start() {
        SetRigidbodies(true);
        SetColliders(false);  
    }

    public void SetRagdoll(bool enabled)
    {
        ragdoll = enabled;
        if (ragdoll) {
            character.enabled = false;
            SetRigidbodies(false);
            SetColliders(true);
        }else{
            character.enabled = true;
            SetRigidbodies(true);
            SetColliders(false);
        }
    }

    public void AddImpact(Vector3 velocity, Vector3 point, float distance)
    {
        if (!ragdoll)
            return;
        Rigidbody[] rigidbodies = character.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies) {
            if (Vector3.Distance(rb.position, point) < distance)
                rb.AddForce(velocity, ForceMode.Impulse);
        }
    }

    void SetRigidbodies(bool kinematic)
    {
        Rigidbody[] rigidbodies = character.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies) {
            rb.isKinematic = kinematic;
        }
    }

    void SetColliders(bool enabled)
    {
        Collider[] colliders = character.gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
            col.enabled = enabled;
        mainCollider.enabled = !enabled;
    }
}
