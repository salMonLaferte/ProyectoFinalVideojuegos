using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChanger : MonoBehaviour
{
    /// <summary>
    /// Name of the gun who change to it
    /// </summary>
    [SerializeField]
    string gunName;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Char>())
        {
            collision.gameObject.GetComponent<Char>().ChangeGun(gunName);
            ObjectActivator act = transform.parent.GetComponent<ObjectActivator>();
            act.StartCoroutine(act.DeactivatePickupCoroutine(gameObject, 3));
        }
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90);
    }
}
