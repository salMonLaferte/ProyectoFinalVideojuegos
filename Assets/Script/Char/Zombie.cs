using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    /// <summary>
    /// Time to wait until checking the player position again in seconds.
    /// </summary>
    float updateFrequency = .05f;
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
        if (patrolArea != null)
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
        return "none";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// Changes the states of the zombie in time intervals.
    /// </summary>
    /// <returns></returns>
    IEnumerator Behavior()
    {
        atackingPlayer = true;
        //random delay in the atack
        yield return new WaitForSeconds(Random.Range(0, .8f));
        while (atackingPlayer)
        {
            stateMachine.ChangeState(new CharMoving(playerReference.transform.position, this));
            yield return new WaitForSeconds(updateFrequency);
        }
        //Go back to its initial position
        stateMachine.ChangeState(new CharMoving(initialPostion, this));
    }
    /// <summary>
    /// Makes the zombie to atack the player.
    /// </summary>
    /// <param name="p"></param>
    protected void AtackPlayer(PatrolArea p)
    {
        //Starts the behavior
        if (!atackingPlayer)
            StartCoroutine(Behavior());
    }
    /// <summary>
    /// Stop the zombie from atacking player
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
}
