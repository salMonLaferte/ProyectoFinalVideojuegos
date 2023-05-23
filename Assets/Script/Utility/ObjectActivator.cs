using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that helps to activate and deactivate objects after a specified time.
/// </summary>
public class ObjectActivator : MonoBehaviour
{
    public IEnumerator DeactivatePickupCoroutine(GameObject powerUp, float time)
    {
        powerUp.SetActive(false);
        yield return new WaitForSeconds(time);
        powerUp.SetActive(true);
    }
}
