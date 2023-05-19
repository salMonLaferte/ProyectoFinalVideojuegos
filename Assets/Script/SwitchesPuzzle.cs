using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchesPuzzle : Puzzle
{
    [SerializeField]
    string rightOrder;

    ArrayList userSolution;

    string[] solution;

    private void Start()
    {
        solution = rightOrder.Split(",");
        userSolution = new ArrayList();
    }

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
