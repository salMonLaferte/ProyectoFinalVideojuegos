using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implements methods to manage the behaviour of a demon enemy.
/// </summary>
public class Demon : Enemy
{
    /// <summary>
    /// Time to wait until checking the player position again in seconds.
    /// </summary>
    float updateFrequency = .05f;
    /// <summary>
    /// Time in seconds that the demon spends walking to the player .
    /// </summary>
    float timeWalking = 1.5f;
    /// <summary>
    /// Time in seconds that the demon spends shooting to the player.
    /// </summary>
    float timeAtacking = 2f;
    /// <summary>
    /// The damage applied by a fire bullet is multiplied by this factor.
    /// </summary>
    float fireVulnerableFactor = 2f;
    /// <summary>
    /// The damage applied by other bullets different from fire is multiplied by this factor.
    /// </summary>
    float otherBulletDamageFactor = .5f;
    /// <summary>
    /// Tells if the demon is in mode of atacking the player.
    /// </summary>
    bool atackingPlayer = false;

    Vector3 initialPostion;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void Start()
    {
        base.Start();
        if(patrolArea != null)
        {
            patrolArea.playerEnteredPatrolArea.AddListener(AtackPlayer);
            patrolArea.playerExitedPatrolArea.AddListener(GoBackToPosition);
        }
        else
        {
            AtackPlayer(patrolArea);
        }
        initialPostion = transform.position;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override string GetInitialGunName()
    {
        return "PistolShortRange";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// Changes the states of the demon in time intervals.
    /// </summary>
    /// <returns></returns>
    IEnumerator Behavior()
    {
        atackingPlayer = true;
        //random delay in the atack
        yield return new WaitForSeconds(Random.Range(0, .8f));
        while (atackingPlayer)
        {
            //Move the caracter in direction to the player
            int cycles = (int)(timeWalking / updateFrequency);
            for(int i=0; i<cycles; i++)
            {
                stateMachine.ChangeState(new CharMoving(playerReference.transform.position, this));
                yield return new WaitForSeconds(updateFrequency);
            }
            cycles = (int)(timeAtacking / updateFrequency);
            //Shoot in directon of the player
            for (int i=0; i<cycles; i++)
            {
                Shoot(playerReference.transform.position);
                yield return new WaitForSeconds(updateFrequency);
            }  
        }
        //Go back to its initial position
        stateMachine.ChangeState(new CharMoving(initialPostion, this));
    }
    /// <summary>
    /// Makes the demon to atack the player.
    /// </summary>
    /// <param name="p"></param>
    protected void AtackPlayer(PatrolArea p)
    {
        //Starts the behavior
        if(!atackingPlayer)
            StartCoroutine(Behavior());
    }
    /// <summary>
    /// Stop the demon from atacking player
    /// </summary>
    /// <param name="p"></param>
    protected void GoBackToPosition(PatrolArea p)
    {
        atackingPlayer = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }
    /// <summary>
    /// Makes the demon vulnerable to fire multiplying the true damage of the bullet for
    /// some factor
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="damageType"></param>
    public override void ReceiveDamage(float amount, Bullet.DamageType damageType)
    {
        if(damageType == Bullet.DamageType.Fire)
        {
            ModifyArmor(amount * fireVulnerableFactor);
        }
        else
        {
            ModifyArmor(amount * otherBulletDamageFactor);
        }
    }
}
