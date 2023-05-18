using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShooting : CharState
{
    Vector3 direction;
    GameObject bullet;
    Char character;
    Transform origin;
    float timer;
    float timeBetwenBullets;

    public CharShooting(Vector3 direction, GameObject bullet, Char character, Transform origin){
        this.direction = direction;
        this.bullet = bullet;
        this.character = character;
        timeBetwenBullets  = .25f;
        this.origin = origin;
    }
    public override void Start()
    {
        base.Start();
        timer = 0;
        character.gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg, Vector3.up);
        GameObject b = GameObject.Instantiate(bullet, origin.position, Quaternion.identity);
        character.GetComponent<Rigidbody>().velocity = new Vector3(0,character.GetComponent<Rigidbody>().velocity.y,0 );
        b.GetComponent<Bullet>().direction = direction;
        
    }

    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if(timer >= timeBetwenBullets ){
            ExitState();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
