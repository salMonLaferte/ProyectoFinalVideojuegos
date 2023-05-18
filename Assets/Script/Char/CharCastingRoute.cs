using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharCastingRoute : CharState
{
    Char character;
    public CharCastingRoute(Char character){
        this.character = character;
    }
    public override void Start()
    {
        base.Start();
        character.GetComponent<Rigidbody>().velocity = new Vector3(0,character.GetComponent<Rigidbody>().velocity.y,0 );
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }


}
