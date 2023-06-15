
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : MonoBehaviour
{
    [SerializeField]
    float amount = 20f;

    [SerializeField]
    AudioSource pickSound;

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().ModifyArmor(amount);
            pickSound.gameObject.transform.SetParent(null);
            pickSound.Play();
            GameObject.Destroy(pickSound.gameObject, 1f);
            GameObject.Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Rotate the object
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90);
    }
}
