using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a door in game
/// </summary>
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        GetComponent<Animator>().SetBool("doorOpened", true);
    }

}
