using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a door in game
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().SetBool("doorOpened", true);
    }

}
