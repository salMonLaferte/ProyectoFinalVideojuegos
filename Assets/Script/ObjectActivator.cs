using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public IEnumerator DeactivatePickupCoroutine(GameObject powerUp, float time)
    {
        powerUp.SetActive(false);
        yield return new WaitForSeconds(time);
        powerUp.SetActive(true);
    }
}
