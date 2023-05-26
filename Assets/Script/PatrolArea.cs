using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// An area which trigger events when player touches it
/// </summary>
public class PatrolArea : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<PatrolArea> playerEnteredPatrolArea = new UnityEvent<PatrolArea>();

    [HideInInspector]
    public UnityEvent<PatrolArea> playerExitedPatrolArea = new UnityEvent<PatrolArea>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredPatrolArea.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerExitedPatrolArea.Invoke(this);
        }
    }
}
