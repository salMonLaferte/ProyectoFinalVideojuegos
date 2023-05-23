using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoving : CharState
{
    Vector3 to;
    Char character;
    Vector3 dir;

    public CharMoving(Vector3 target, Char character){ 
        to = target;
        this.character = character;
    }

    public override void Start()
    {
        base.Start();
        dir = VectorTools.DirectionXZ(character.gameObject.transform.position, to).normalized;
        character.gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.x, dir.z)*Mathf.Rad2Deg, Vector3.up);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(VectorTools.DistanceXZ(character.gameObject.transform.position,to) >= .5f){
            character.GetComponent<Rigidbody>().velocity =  dir * character.GetSpeed() + new Vector3(0,character.GetComponent<Rigidbody>().velocity.y,0);
        }
        else{
            ExitState();
            character.GetComponent<Rigidbody>().velocity =  Vector3.zero;
        }
    }
}
