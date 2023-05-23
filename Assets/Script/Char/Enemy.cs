using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Char
{
    protected GameObject playerReference;
    protected override void Start()
    {
        base.Start();
        playerReference = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }
}
