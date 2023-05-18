using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharStateMachine 
{
    CharState current;
    CharState defaultState;
    public UnityEvent currentStateFinished = new UnityEvent();

    bool isStunned = false;

    public CharStateMachine(CharState defaultState){
        current = defaultState;
        this.defaultState = defaultState;
        isStunned = false;
    }
    public void ChangeState(CharState state){
        if(isStunned)
            return;
        if(state is CharStunned)
            isStunned = true;
        current.ExitState();
        current = state;
        current.Start();
        current.stateFinished.AddListener(OnCurrentStateFinished);
    }

    public void OnCurrentStateFinished(){
        current = defaultState;
        currentStateFinished.Invoke();
        currentStateFinished.RemoveAllListeners();
    }

    public CharState GetCurrent(){
        return current;
    }

    public void StopStun(){
        isStunned = false;
        ChangeToDefault();
    }

    public void ChangeToDefault(){
        ChangeState(defaultState);
    }
}
