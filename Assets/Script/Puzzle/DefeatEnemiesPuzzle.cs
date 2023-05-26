using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Puzzle which consist in 
/// </summary>
public class DefeatEnemiesPuzzle : Puzzle
{
    /// <summary>
    /// List of enemies that are required to eliminate to 
    /// complete the puzzle
    /// </summary>
    public List<Enemy> enemies;
    /// <summary>
    /// Count of how many enemies has been defeated.
    /// </summary>
    int count = 0;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    private void Start()
    {
        foreach( Enemy enemy in enemies)
        {
            enemy.characterDied.AddListener(registerEnemy);
        }
    }

    /// <summary>
    /// Register that an enemy has been defeated
    /// </summary>
    /// <param name="character"></param>
    void registerEnemy(Char character)
    {
        count++;
        if(count >= enemies.Count)
        {
            PuzzleFinished();
        }
    }
}
