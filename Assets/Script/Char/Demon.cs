using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Enemy
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Behavior());
    }

    public override string GetInitialGunName()
    {
        return "PistolShortRange";
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    IEnumerator Behavior()
    {
        while (true)
        {
            for(int i=0; i<15; i++)
            {
                stateMachine.ChangeState(new CharMoving(playerReference.transform.position, this));
                yield return new WaitForSeconds(.1f);
            }
            for(int i=0; i<10; i++)
            {
                Shoot(playerReference.transform.position);
                yield return new WaitForSeconds(.1f);
            }
           
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
