using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Property to add to objects that are destroyed by rockets
/// </summary>
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
