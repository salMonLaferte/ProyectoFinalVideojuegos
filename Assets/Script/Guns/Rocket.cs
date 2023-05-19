using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField]
    GameObject explosion;

    [SerializeField]
    GameObject explosionParticles;

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.gameObject.CompareTag("Environment"))
        {
            GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Instantiate(explosionParticles, transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }
}
