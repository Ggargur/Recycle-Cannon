using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class WalkingEntity : MonoBehaviour
{
    [Header("Walking Entity Values")]
    [SerializeField] protected NavMeshAgent _navAgent;
    public Vector3 Destination;
    protected void FixedUpdate()
    {
        transform.LookAt(Destination);
        _navAgent.destination = Destination;
    }
}
