using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IGun
{
    /// <summary>
    /// Cooldown of the pistol
    /// </summary>
    float cooldown = .4f;
    /// <summary>
    /// Damage of the gun
    /// </summary>
    float damage = 5;
    /// <summary>
    /// Speed of the bullets in units/second
    /// </summary>
    float speed = 20;
    /// <summary>
    /// Bullet that is shoot by this gun
    /// </summary>
    [SerializeField]
    GameObject bullet;
    /// <summary>
    /// Position from where the bullet is shoot
    /// </summary>
    [SerializeField]
    GameObject bulletOrigin;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public float GetCooldown()
    {
        return cooldown;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public float GetDamage()
    {
        return damage;
    }
    
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public float GetSpeed()
    {
        return speed;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public float Shoot()
    {
        throw new System.NotImplementedException();
    }
}
