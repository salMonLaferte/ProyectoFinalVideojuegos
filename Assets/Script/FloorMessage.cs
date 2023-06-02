using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMessage : MonoBehaviour
{
    [SerializeField]
    int level = 0;
    void Start()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text += "\n" + level;
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        GameObject.Destroy(gameObject);
    }
}
