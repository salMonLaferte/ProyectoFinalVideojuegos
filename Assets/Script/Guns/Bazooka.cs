using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Bazooka : MonoBehaviour, IGun
{
    /// <summary>
    /// Name of the gun
    /// </summary>
    new string name = "Bazooka";
    /// <summary>
    /// Cooldown of the pistol
    /// </summary>
    float cooldown = 1;
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
    /// Is the weapon on coolDown
    /// </summary>
    bool onCooldown = false;
    /// <summary>
    /// Who can damage this weapon
    /// </summary>
    Bullet.AppliesDamageTo appliesDamageTo;

    [SerializeField]
    int recoil = 150;

    [SerializeField]
    AudioClip shoot;

    private void Start()
    {
        StartCoroutine(Cooldown(.5f));
    }

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
    public string GetName()
    {
        return name;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public int Shoot(Vector3 selectedPoint)
    {
        if (onCooldown)
            return -1;
        StartCoroutine(Cooldown(1));
        Vector3 dir = VectorTools.DirectionXZ(bulletOrigin.transform.position, selectedPoint);
        GameObject b = GameObject.Instantiate(bullet, bulletOrigin.transform.position, transform.rotation);
        b.GetComponent<Bullet>().Initialize(appliesDamageTo, dir);
        GetComponent<AudioSource>().clip = shoot;
        GetComponent<AudioSource>().Play();
        return 1;
    }

    /// <summary>
    /// Cooldown coroutine that sets the pistol on cooldown for the amount of seconds specified
    /// </summary>
    /// <returns></returns>
    IEnumerator Cooldown(float factor)
    {
        onCooldown = true;
        yield return new WaitForSeconds(factor*cooldown);
        onCooldown = false;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Bullet.AppliesDamageTo GetAppliesDamageTo()
    {
        return appliesDamageTo;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="damageTo"></param>
    public void SetAppliesDamageTo(Bullet.AppliesDamageTo damageTo)
    {
        appliesDamageTo = damageTo;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public int GetRecoil()
    {
        return recoil;
    }
}
