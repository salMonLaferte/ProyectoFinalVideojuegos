using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ArmorBar : MonoBehaviour
{
    public Char characterRef;
    
    GameObject cameraRef;

    void Awake(){
        
    }

    void Start(){
        characterRef.armorChanged.AddListener(OnCharacterRef_healthChaged);
        cameraRef = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnCharacterRef_healthChaged( float health, float maxHealth ){
        GetComponent<SpriteRenderer>().material.SetFloat("percent",health/maxHealth);
    }

    void Update(){
        transform.rotation = Quaternion.LookRotation(cameraRef.transform.position - transform.position , Vector3.up);
    }


}
