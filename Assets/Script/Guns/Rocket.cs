using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implements methods to control a rocket which explodes when touching an environment object.
/// </summary>
public class Rocket : Bullet
{
    [SerializeField]
    GameObject explosion;

    [SerializeField]
    GameObject explosionParticles;

    [SerializeField]
    AudioSource explosionSoundEffect;

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.gameObject.CompareTag("Environment") || col.gameObject.GetComponent<Enemy>() != null)
        {
            GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Instantiate(explosionParticles, transform.position, Quaternion.identity);
            explosionSoundEffect.Play();
            explosionSoundEffect.transform.parent = null;
            GameObject.Destroy(explosionSoundEffect, 1);
            GameObject.Destroy(gameObject);
        }
    }
}
