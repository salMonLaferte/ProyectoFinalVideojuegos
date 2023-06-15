using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [TextAreaAttribute(0,10)]
    public string dialog = "...";

    bool rotate = false;

    bool canPick = true;

    private void Start()
    {
        if (gameObject.CompareTag("Book"))
            rotate = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canPick)
        {
            StartCoroutine(delayToTouchAgain());
            GameObject dialogBox = GameObject.Instantiate((UnityEngine.GameObject)Resources.Load("Dialog"), transform.position, Quaternion.identity);
            dialogBox.GetComponent<Dialog>().SetUp(dialog);
            if (gameObject.CompareTag("Book"))
            {
                GameManager.BookPickedUp();
            }else
                GameObject.Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Rotate the object
        if (rotate)
        {
            transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90);
        }  
    }

    IEnumerator delayToTouchAgain()
    {
        canPick = false;
        yield return new WaitForSeconds(1);
        canPick = true;
    }
}
