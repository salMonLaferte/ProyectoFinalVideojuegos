using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Puzzle : MonoBehaviour
{
    [SerializeField]
    UnityEvent<Puzzle> puzzleFinished;

    [SerializeField]
    UnityEvent<Puzzle> puzzleFailed;

    protected void PuzzleFinished()
    {
        puzzleFinished.Invoke(this);
    }

    protected void PuzzleFailed()
    {
        puzzleFailed.Invoke(this);
    }
}
