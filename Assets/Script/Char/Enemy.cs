using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for enemies of the game
/// </summary>
public class Enemy : Char
{
    /// <summary>
    /// Reference to the player object
    /// </summary>
    protected GameObject playerReference;

    [SerializeField]
    protected PatrolArea patrolArea;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void Start()
    {
        base.Start();
        playerReference = GameObject.FindGameObjectWithTag("Player");
        currentGun.SetAppliesDamageTo(Bullet.AppliesDamageTo.Player);
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }
}
