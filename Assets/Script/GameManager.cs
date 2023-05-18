using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  GameManager 
{
    public static CheckPoint currentCheckpoint;
    public static void OnPlayerDied(Char player){
        if(currentCheckpoint != null ){
            player.RespawnOnPoint(currentCheckpoint.GetRespawnPoint());
        }
        
    }
}
