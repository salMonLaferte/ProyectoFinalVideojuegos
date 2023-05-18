using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Base class for everyState
/// </summary>
public class CharState
{
    /// <summary>
    /// State by default on every character
    /// </summary>
    public static CharIddle defaultState = new CharIddle();
    /// <summary>
    /// Event that tell to listeners when the state is finished
    /// </summary>
    public  UnityEvent stateFinished = new UnityEvent();
    /// <summary>
    /// Method that is called when the character change to this state
    /// </summary>
    public  virtual void Start(){

    }
    /// <summary>
    /// Method that is called in every frame when the state is actived.
    /// </summary>
    public virtual void Update(){

    }
    /// <summary>
    /// Method that is called in every physics frame when the state is activated
    /// </summary>
    public virtual void FixedUpdate(){

    }
    /// <summary>
    /// Method that is called everytime a character exits the state
    /// </summary>
    public virtual void ExitState(){
        stateFinished.Invoke();
    }
}
