using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Arsenal with the set of guns available
/// </summary>
public class GunSelector
{
    /// <summary>
    /// Available weapons in the arsenal
    /// </summary>
    string[] guns = { "Pistol", "RocketLauncher", "FlameThrower" };

    int currentWeapon = 0;
    /// <summary>
    /// Returns the name of the next weapon in the arsenal
    /// </summary>
    /// <returns></returns>
    public string NextWeapon()
    {
        currentWeapon++;
        if(currentWeapon >= guns.Length)
        {
            currentWeapon = 0;
        }
        return guns[currentWeapon];
    }
    /// <summary>
    /// Returns the name of the previous weapon in the arsenal
    /// </summary>
    /// <returns></returns>
    public string PreviousWeapon()
    {
        currentWeapon--;
        if(currentWeapon < 0)
        {
            currentWeapon = guns.Length - 1;
        }
        return guns[currentWeapon];
    }
    /// <summary>
    /// Returns the name of the current weapon
    /// </summary>
    /// <returns></returns>
    public string GetWeapon()
    {
        return guns[currentWeapon];
    }
}
