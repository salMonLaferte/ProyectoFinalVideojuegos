using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Char
{
    [SerializeField]
    GameObject bullet;
    
    [SerializeField]
    Bullet.AppliesDamageTo appliesDamageTo = Bullet.AppliesDamageTo.Enemys;

    [SerializeField]
    Vector3 bufferedClick;

    [SerializeField]
    GameObject clickIndicatorPrefab;

    GameObject clickIndicator;

    [SerializeField]
    public Transform bulletOrigin;

    protected override void Start()
    {
        base.Start();
        characterDied.AddListener(GameManager.OnPlayerDied);
        ChangeGun("FlameThrower");
    }

    protected override void Update()
    {
        base.Update();
        Vector3 selectedPoint = GetSelectedGroundPoint();
        if(Input.GetMouseButton(1) && (stateMachine.GetCurrent() is CharIddle || stateMachine.GetCurrent() is CharMoving)){
            stateMachine.ChangeState( new CharMoving(selectedPoint, this, speed ) );
            bufferedClick = Vector3.zero;
        }
        if(!Input.GetMouseButton(1) && bufferedClick != Vector3.zero && (stateMachine.GetCurrent() is CharIddle || stateMachine.GetCurrent() is CharMoving)){
            selectedPoint = bufferedClick;
            stateMachine.ChangeState( new CharMoving(selectedPoint, this, speed ) );

            bufferedClick = Vector3.zero;
        }
        //Shoot current gun
        if(Input.GetMouseButton(0) && (stateMachine.GetCurrent() is CharIddle || stateMachine.GetCurrent() is CharMoving || stateMachine.GetCurrent() is CharShooting) )
        {
            Vector3 dir = VectorTools.DirectionXZ(bulletOrigin.position, selectedPoint);
            stateMachine.ChangeState(new CharShooting(dir, this));
            currentGun.Shoot(selectedPoint);
            GameObject.Destroy(clickIndicator);
        }
        if(stateMachine.GetCurrent() is CharShooting && Input.GetMouseButton(1)){
            bufferedClick = selectedPoint;
        }
    }

    void OnClick(Vector3 point){
        GameObject.Destroy(clickIndicator);
        clickIndicator = GameObject.Instantiate(clickIndicatorPrefab, point, Quaternion.identity);
    }

    protected override void FixedUpdate(){
        base.FixedUpdate();
    }

    public override void ChangeGun(string name)
    {
        base.ChangeGun(name);
        currentGun.SetAppliesDamageTo(appliesDamageTo);
    }
}
