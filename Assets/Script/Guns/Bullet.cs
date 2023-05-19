using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Speed of the bullet in units per second
    /// </summary>
    protected float speed;
    [SerializeField]
    /// <summary>
    /// How far the bullet can get
    /// </summary>
    protected float range;
    /// <summary>
    /// Direction of the bullet, needs to be normalized if is changed
    /// </summary>
    [HideInInspector]
    protected Vector3 direction; 
    /// <summary>
    /// Type of characters that this bullet aplies damage to
    /// </summary>
    [HideInInspector]
    protected AppliesDamageTo appliesDamageTo;
    [SerializeField]
    /// <summary>
    /// Damage of the bullet
    /// </summary>
    protected float damage;
    /// <summary>
    /// Rigidbody of this object
    /// </summary>
    Rigidbody rb;
    /// <summary>
    /// Timer to keep track of how much time the bullet has been alive and destroy it when necessary
    /// </summary>
    protected float timer = 0;


    protected virtual void Start(){
        rb = GetComponent<Rigidbody>();
        direction.Normalize();
    }

    /// <summary>
    /// Method to call when the bullet is shot
    /// </summary>
    /// <param name="appliesDamageTo"></param>
    /// <param name="direction"></param>
    public virtual void Initialize(Bullet.AppliesDamageTo appliesDamageTo, Vector3 direction)
    {
        this.appliesDamageTo = appliesDamageTo;
        this.direction = direction;
    }

    /// <summary>
    /// Moves the bullet
    /// </summary>
    protected virtual void FixedUpdate(){
        rb.MovePosition( rb.position + Time.deltaTime * speed * direction );
        if(timer * speed >= range )
            GameObject.Destroy(gameObject);
        timer += Time.deltaTime;

    }

    /// <summary>
    /// If bullet collides with a character applies damage if necessary
    /// </summary>
    /// <param name="col"></param>
    protected virtual void OnTriggerEnter(Collider col){
        Char refChar = col.gameObject.GetComponent<Char>();
        if(damage > 0){
            if(refChar !=null ){
                if( Char.ShouldTakeDamage(refChar, appliesDamageTo)){
                    refChar.ModifyArmor(-damage);
                }
            }
        }
    }

    /// <summary>
    /// Tag for a bullet to apply damage to specific characters.
    /// </summary>
    public enum AppliesDamageTo{
        Enemys = 0,
        Player = 1,
        Everyone = 2
    }
}
