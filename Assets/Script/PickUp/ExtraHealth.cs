
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : MonoBehaviour
{
    [SerializeField]
    float amount = 20f;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().ModifyArmor(amount);
            GameObject.Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Rotate the object
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90);
    }
}
