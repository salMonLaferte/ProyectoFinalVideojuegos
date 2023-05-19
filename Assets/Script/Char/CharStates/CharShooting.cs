using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShooting : CharState
{
    /// <summary>
    /// Direction in which is shooting
    /// </summary>
    Vector3 direction;
    /// <summary>
    /// Origin character
    /// </summary>
    Char character;

    public CharShooting(Vector3 direction, Char character){
        this.direction = direction;
        this.character = character;
    }
    public override void Start()
    {
        //Rotates the character to face the place is shooting and make him stay still
        base.Start();
        character.gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg, Vector3.up);
        character.GetComponent<Rigidbody>().velocity = new Vector3(0,character.GetComponent<Rigidbody>().velocity.y,0 );
    }

    public override void Update()
    {
        //If the left mouse click is not pressed player is not shooting anymore
        base.Update();
        if (!Input.GetMouseButton(0))
            ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
