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

    IGun currentGun;

    [SerializeField]
    public Transform bulletOrigin;

    protected override void Start()
    {
        base.Start();
        bullet.GetComponent<Bullet>().appliesDamageTo = appliesDamageTo;
        characterDied.AddListener(GameManager.OnPlayerDied);
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
        if(Input.GetMouseButton(0) && (stateMachine.GetCurrent() is CharIddle || stateMachine.GetCurrent() is CharMoving) )
        {
            Vector3 dir = VectorTools.DirectionXZ(bulletOrigin.position, selectedPoint);
            stateMachine.ChangeState(new CharShooting(dir, bullet, this, bulletOrigin));
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
}
