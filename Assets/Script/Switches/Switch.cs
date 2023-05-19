using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    bool activated = false;

    [SerializeField]
    UnityEvent<Switch> onSwitchActivated;

    [SerializeField]
    Material activatedSwitch;

    [SerializeField]
    Material notActivatedSwitch;

    [SerializeField]
    MeshRenderer switchModel;

    bool cantBeActivate;

    private void Start()
    {
        if (activated)
            switchModel.material = activatedSwitch;
        else
            switchModel.material = notActivatedSwitch;
    }

    public void Activate()
    {
        if (!activated && !cantBeActivate)
        {
            activated = true;
            switchModel.material = activatedSwitch;
            onSwitchActivated.Invoke(this);
        }
    }

    public void Deactivate()
    {
        if (activated)
        {
            activated = false;
            switchModel.material = notActivatedSwitch;
            StartCoroutine(delayToActiveCoroutine());
        }
    }

    IEnumerator delayToActiveCoroutine()
    {
        cantBeActivate = true;
        yield return new WaitForSeconds(.25f);
        cantBeActivate = false;
    }
}
