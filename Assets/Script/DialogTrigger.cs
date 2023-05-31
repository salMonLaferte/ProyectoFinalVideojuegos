using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    string dialog = "...";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject dialogBox = GameObject.Instantiate((UnityEngine.GameObject)Resources.Load("Dialog"), transform.position, Quaternion.identity);
            dialogBox.GetComponent<Dialog>().SetUp(dialog);
            GameObject.Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Rotate the object
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90);
    }
}
