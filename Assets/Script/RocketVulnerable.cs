using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Property to add to objects that are destroyed by rockets
/// </summary>
public class RocketVulnerable : MonoBehaviour
{
    [SerializeField]
    GameObject destroyParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rocket>() != null)
        {
            GameObject.Instantiate(destroyParticles, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }
}
