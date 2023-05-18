using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for Guns
/// </summary>
public interface IGun
{
    /// <summary>
    /// Damage of the gun
    /// </summary>
    abstract float GetDamage();
    /// <summary>
    /// Speed of the proyectile
    /// </summary>
    abstract float GetSpeed();
    /// <summary>
    /// Time pefore shooting a new bullet in seconds
    /// </summary>
    abstract float GetCooldown();
    /// <summary>
    /// Shots the weapon
    /// </summary>
    /// <returns></returns>
    abstract float Shoot();
}
