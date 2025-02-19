using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Implements methods to control the general flow of the game
/// </summary>
public static class  GameManager 
{
    /// <summary>
    /// Current checkpoint to respawn the player
    /// </summary>
    public static CheckPoint currentCheckpoint;

    public static int bookCount;

    /// <summary>
    /// Function to call when the player dies:
    /// * Respawn the player in the last checkpoint
    /// </summary>
    /// <param name="player"></param>
    public static void OnPlayerDied(Char player){
        if(currentCheckpoint != null ){
            player.RespawnOnPoint(currentCheckpoint.GetRespawnPoint());
        }
        GameObject gameOver = GameObject.Instantiate((UnityEngine.GameObject)Resources.Load("GameOver"), Vector3.zero, Quaternion.identity);
    }

    public static void BookPickedUp()
    {
        bookCount++;
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

