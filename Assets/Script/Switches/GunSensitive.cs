using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Specifies with what kind of bullet the switch is activated
/// </summary>
[RequireComponent(typeof(Switch))]
public class GunSensitive : MonoBehaviour
{
    [SerializeField]
    GunType gun;
    /// <summary>
    /// Names of the bullets mapped to the enum of the gun types.
    /// </summary>
    static string[] bulletNames = { "PistolBullet", "Rocket", "Flame" };

    private void Start()
    {
        /*if (gun == GunType.Pistol)
            bulletName = "PistolBullet";
        if (gun == GunType.RocketLauncher)
            bulletName = "Rocket";
        if (gun == GunType.FlameThrower)
            bulletName = "Flame";*/
    }

    [SerializeField]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(bulletNames[(int)gun]))
        {
            GetComponent<Switch>().Activate();
        }
    }
}
/// <summary>
/// GunTypes
/// </summary>
public enum GunType
{
    Pistol = 0,
    RocketLauncher = 1,
    FlameThrower = 2
}
