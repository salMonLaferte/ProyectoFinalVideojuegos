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
    abstract float GetCooldown();
    /// <summary>
    /// Shots the weapon
    /// </summary>
    /// <returns></returns>
    abstract void Shoot(Vector3 selectedPoint);
    /// <summary>
    /// Returns name of the weapon, should be same as prefab object of the weapon
    /// </summary>
    /// <returns></returns>
    abstract string GetName();
    /// <summary>
    /// Return the type of character this gun damages
    /// </summary>
    /// <returns></returns>
    abstract Bullet.AppliesDamageTo GetAppliesDamageTo();
    /// <summary>
    /// Sets who can take damage from this gun
    /// </summary>
    /// <returns></returns>
    abstract void SetAppliesDamageTo(Bullet.AppliesDamageTo damageTo);
}
