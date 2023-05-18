using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDashing : CharState
{
    float dashForce;
    Rigidbody bd;
    Vector3 dir;
    float timer=0;

    Char character;
    public CharDashing(float force, Vector3 dir, Char character){
        dashForce = force;
        this.bd = character.GetComponent<Rigidbody>();
        this.dir=dir;
        this.character = character;
    }
    
    public override void Start(){
        base.Start();
        timer=0;
        bd.AddForce(dashForce * dir,ForceMode.Impulse);
        character.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.x, dir.z)*Mathf.Rad2Deg, Vector3.up);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        timer += Time.deltaTime;
        if(timer >.25f ){
            bd.velocity = Vector3.zero;
            stateFinished.Invoke();

        }
            

    }

}
