using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Implements methods to manage the consequences of the activation of a switch
/// </summary>
public class Switch : MonoBehaviour
{
    /// <summary>
    /// Tells if this switch is activated.
    /// </summary>
    bool activated = false;
    /// <summary>
    /// Event that is invoked when the switch is activated.
    /// </summary>
    [SerializeField]
    UnityEvent<Switch> onSwitchActivated;
    /// <summary>
    /// Material for this switch when is activated.
    /// </summary>
    [SerializeField]
    Material activatedSwitch;
    /// <summary>
    /// Material for this switch when is not activated.
    /// </summary>
    [SerializeField]
    Material notActivatedSwitch;
    /// <summary>
    /// Mesh that modifys material depending of the switch state.
    /// </summary>
    [SerializeField]
    MeshRenderer switchModel;
    /// <summary>
    /// Tells if the switch cant be activate at the moment
    /// </summary>
    bool cantBeActivate;

    private void Start()
    {
        //Update material of the switch
        if (activated)
            switchModel.material = activatedSwitch;
        else
            switchModel.material = notActivatedSwitch;
    }
    /// <summary>
    /// Invokes the onSwitchActivated event, and change material
    /// </summary>
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
            //After the switch is deactivated wait until it can be activated agan
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
