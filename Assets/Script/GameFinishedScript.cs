using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishedScript : MonoBehaviour
{
    [SerializeField]
    GameObject message;

    public void onFinalDialogShown()
    {
        message.SetActive(true);
    }
}
