using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Base class for puzzles
/// </summary>
public class Puzzle : MonoBehaviour
{
    /// <summary>
    /// Event invoked when the puzzle is finished.
    /// </summary>
    [SerializeField]
    UnityEvent<Puzzle> puzzleFinished;

    /// <summary>
    /// Event to invoke when the puzzle is failed and set up needs to be restarted.
    /// </summary>
    [SerializeField]
    UnityEvent<Puzzle> puzzleFailed;

    /// <summary>
    /// Function to call when the puzzle is finished.
    /// </summary>
    protected void PuzzleFinished()
    {
        puzzleFinished.Invoke(this);
    }

    /// <summary>
    /// Function to call when the puzzle is failed.
    /// </summary>
    protected void PuzzleFailed()
    {
        puzzleFailed.Invoke(this);
    }
}
