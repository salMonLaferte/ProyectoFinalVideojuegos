using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShooting : CharState
{
    /// <summary>
    /// Direction in which is shooting
    /// </summary>
    Vector3 selectedPoint;
    /// <summary>
    /// Origin character
    /// </summary>
    Char character;
    /// <summary>
    /// Time that the character stays shooting
    /// </summary>
    float timeShooting = -1;

    bool shootingIsActive = true;

    public CharShooting(Vector3 selectedPoint, Char character){
        this.selectedPoint = selectedPoint;
        this.character = character;
    }

    public CharShooting(Vector3 selectedPoint, Char character, float time)
    {
        this.selectedPoint = selectedPoint;
        this.character = character;
        timeShooting = time;
    }

    public override void Start()
    {
        //Rotates the character to face the place is shooting and make him stay still
        base.Start();
        Vector3 dir = VectorTools.DirectionXZ(character.transform.position, selectedPoint);
        character.gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.x, dir.z)*Mathf.Rad2Deg, Vector3.up);
        character.GetComponent<Rigidbody>().velocity = new Vector3(0,character.GetComponent<Rigidbody>().velocity.y,0 );
        character.StartCoroutine(ShootingCoroutine());
        Animator anim = character.GetComponent<Animator>();
    }

    public override void Update()
    {
        //If the left mouse click is not pressed player is not shooting anymore
        base.Update();
        if (!Input.GetMouseButton(0) && character is Player)
        {
            ExitState();
        }
    }
    /// <summary>
    /// Coroutine that keep the character shooting in an efficient way, shoot function in the gun is called
    /// accordingly to the fire rate of the gun.
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootingCoroutine()
    {
        while (shootingIsActive)
        {
            int result = character.GetGun().Shoot(selectedPoint);
            if (result == 1)
            {
                Vector3 dir = VectorTools.DirectionXZ(character.transform.position, selectedPoint);
                character.GetComponent<Rigidbody>().AddForce( -character.GetGun().GetRecoil() * dir.normalized, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(character.GetGun().GetCooldown());
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void ExitState()
    {
        shootingIsActive = false;//set the variable that stops the shooting coroutine to false
        base.ExitState();
    }

}
