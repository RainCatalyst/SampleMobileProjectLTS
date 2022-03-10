using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutodestroy : MonoBehaviour
{
    [SerializeField] float time = 1;

    void Start()
    {
        Destroy(gameObject, time);
    }
}
