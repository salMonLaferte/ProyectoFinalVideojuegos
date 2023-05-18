using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    bool setThisCheckPointAsDefault = false;

    [SerializeField]
    Transform respawnPoint;    
    void Start(){
        if(setThisCheckPointAsDefault)
            GameManager.currentCheckpoint = this;
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.GetComponent<Player>() != null ){
            GameManager.currentCheckpoint = this;
        }
    }

    public Vector3 GetRespawnPoint(){
        return respawnPoint.position;
    }

}
