using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Puzzle which consist in the activation of many switches or events.
/// </summary>
public class SwitchesPuzzle : Puzzle
{
    /// <summary>
    /// The right order of activation of the switches or events.
    /// The format for the solution is a set of ids separated by the "," character.
    /// Example:2,3,4,1
    /// </summary>
    [SerializeField]
    string rightOrder;
    /// <summary>
    /// Partial solution of the puzzle.
    /// </summary>
    ArrayList userSolution;
    /// <summary>
    /// Solution of the puzzle as an array.
    /// </summary>
    string[] solution;

    private void Start()
    {
        solution = rightOrder.Split(",");
        userSolution = new ArrayList();
    }
     /// <summary>
     /// Registers the activation of a switch with the specified Id, the puzzle
     /// will be correct if the order of ids matches the rightOrder string
     /// </summary>
     /// <param name="id"></param>
    public void registerSwitch(int id)
    {
        userSolution.Add(id.ToString());
        if(userSolution.Count == solution.Length)
        {
            bool isCorrect = true;
            for(int i=0; i<solution.Length; i++)
            {
                if( solution[i] != (string)userSolution[i])
                {
                    isCorrect = false;
                    break;
                }
            }
            if (isCorrect)
            {
                PuzzleFinished();
            }
            else
            {
                PuzzleFailed();
                userSolution = new ArrayList();
            }

        }
    }
}
