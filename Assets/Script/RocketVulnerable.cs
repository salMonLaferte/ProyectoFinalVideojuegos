using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketVulnerable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rocket>() != null)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
