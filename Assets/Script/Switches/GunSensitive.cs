using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Switch))]
public class GunSensitive : MonoBehaviour
{
    [SerializeField]
    GunType gun;

    string bulletName;

    private void Start()
    {
        if (gun == GunType.Pistol)
            bulletName = "PistolBullet";
        if (gun == GunType.RocketLauncher)
            bulletName = "Rocket";
        if (gun == GunType.FlameThrower)
            bulletName = "Flame";
    }

    [SerializeField]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(bulletName))
        {
            GetComponent<Switch>().Activate();
        }
    }
}

public enum GunType
{
    Pistol = 0,
    RocketLauncher = 1,
    FlameThrower = 2
}
