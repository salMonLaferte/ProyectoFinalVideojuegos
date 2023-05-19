using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Char : MonoBehaviour
{
    [Space(20)]

    [Header("Character settings")]
    
    [SerializeField]
    LayerMask GroundLayer;

    //Character Stats
    [SerializeField]
    protected float speed = 10;

    [SerializeField]
    protected float maxArmor = 100; 

    protected float armor;

    protected bool isStunned = false;
    
    protected Vector3 movingTo;
    
    protected CharStateMachine stateMachine;

    protected IGun currentGun;

    [SerializeField]
    protected GameObject gunPosition;

    [SerializeField]
    protected GameObject gun;

    [SerializeField]
    protected GameObject healthBarPrefab;
    
    protected bool damageCoroutineActive = false;

    [HideInInspector]
    public UnityEvent<Char> characterDied = new UnityEvent<Char>();
    
    [HideInInspector]
    public UnityEvent<float,float> armorChanged = new UnityEvent<float,float>();


    
    protected virtual void Awake(){
        if(healthBarPrefab !=null ){
             GameObject bar = GameObject.Instantiate(healthBarPrefab, transform.position + new Vector3(0,6,0), Quaternion.identity);
             bar.GetComponent<ArmorBar>().characterRef = this;
             bar.transform.SetParent(transform);
        }
    }

    protected virtual void Start(){
        movingTo = transform.position;
        stateMachine = new CharStateMachine(new CharIddle());
        armor = maxArmor;
    }

    protected virtual void Update()
    {
        stateMachine.GetCurrent().Update();
    }

    protected virtual void FixedUpdate(){
        Rigidbody rb = GetComponent<Rigidbody>();
        if( rb.velocity.y > 1 ){
            rb.velocity = new Vector3(rb.velocity.x, 1, rb.velocity.z);
        }
        stateMachine.GetCurrent().FixedUpdate();
        Vector3[] rayOrigins = new Vector3[5];
        BoxCollider box = GetComponent<BoxCollider>();
        float parameter = 1.25f;
        rayOrigins[0] = transform.position + box.center + new Vector3(-parameter* box.size.x/2,0, parameter * box.size.z/2);
        rayOrigins[1] = transform.position + box.center + new Vector3(-parameter *box.size.x/2,0, -parameter * box.size.z/2);
        rayOrigins[2] = transform.position + box.center + new Vector3(parameter* box.size.x/2, 0, parameter * box.size.z/2);
        rayOrigins[3] = transform.position + box.center +new Vector3(parameter* box.size.x/2,0, -parameter * box.size.z/2);
        rayOrigins[4] = transform.position;
        Ray ray = new Ray(transform.position, Vector3.down);
        bool flag = false;
        for(int i=0; i< rayOrigins.Length; i++){
            if(Physics.Raycast(rayOrigins[i], Vector3.down, 1.2f, GroundLayer)){
                flag = true; 
                break;
            }
        }
        if(!flag){
            rb.velocity = new Vector3(0, -9, 0);
        }
    }

    protected virtual void OnCollisionEnter(Collision col)
    {

    }

    protected Vector3 GetSelectedGroundPoint(){
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(r, out hit, 1000, GroundLayer);
        return hit.point;
    }

    public void Stun(float time){
        if(!(stateMachine.GetCurrent() is CharStunned)){
            StartCoroutine(StunCoroutine(time));
        }
    }

    IEnumerator StunCoroutine(float time){
        stateMachine.ChangeState( new CharStunned() );
        yield return new WaitForSeconds(time);
        stateMachine.StopStun();
    }

    public void ModifyArmor(float amount){
        armor += amount;
        if( armor > maxArmor)
            armor = maxArmor;
        if(armor < 0)
            armor = 0;
        armorChanged.Invoke(armor, maxArmor);
        if(amount < 0 && !damageCoroutineActive)
            StartCoroutine(ChangeMaterialForDamage());
        if(armor <= 0){
            characterDied.Invoke(this);
            if( gameObject.GetComponent<Player>() == null )
                GameObject.Destroy(gameObject); 
        }
    }

    IEnumerator ChangeMaterialForDamage(){
        damageCoroutineActive = true;
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        for(int i=0; i< meshes.Length; i++){
             meshes[i].material.SetFloat("Hurt", 1);
        }
        yield return new WaitForSeconds(.1f);
        for(int i=0; i<meshes.Length; i++){
            meshes[i].material.SetFloat("Hurt", 0);
        }
        damageCoroutineActive = false;
    }

    /// <summary>
    /// Tells if a character should take damage
    /// </summary>
    /// <param name="c"></param>
    /// <param name="appliesDamageTo"></param>
    /// <returns></returns>
    public static bool ShouldTakeDamage( Char c, Bullet.AppliesDamageTo appliesDamageTo){
        if(appliesDamageTo == Bullet.AppliesDamageTo.Everyone)
            return true;
        if( c is Player && appliesDamageTo == Bullet.AppliesDamageTo.Player)
            return true;
        if( c is Enemy && appliesDamageTo == Bullet.AppliesDamageTo.Enemys)
            return true;
        return false;
    }

    /// <summary>
    /// Returns the max armor of the character
    /// </summary>
    /// <returns></returns>
    public float GetMaxArmor(){
        return maxArmor;
    }

    /// <summary>
    /// Restores an amount of armor
    /// </summary>
    public void RestoreArmor(){
        ModifyArmor(maxArmor);
    }

    /// <summary>
    /// Respawns the character in a determined position
    /// </summary>
    /// <param name="position"></param>
    public void RespawnOnPoint(Vector3 position){
        RestoreArmor();
        stateMachine.ChangeState(new CharIddle());
        transform.position = position;
    }

    /// <summary>
    /// Change the gun of the character
    /// </summary>
    /// <param name="name"></param>
    public virtual void ChangeGun(string name)
    {
        GameObject gunPrefab = GameObject.Instantiate((UnityEngine.GameObject)Resources.Load(name), gunPosition.transform.position, transform.rotation);
        gunPrefab.transform.SetParent(transform);
        GameObject.Destroy(gun);
        gun = gunPrefab;
        if (name == "Pistol")
            currentGun = (IGun)gunPrefab.GetComponent<Pistol>();
        if (name == "RocketLauncher")
            currentGun = (IGun)gunPrefab.GetComponent<Bazooka>();
        if (name == "FlameThrower")
            currentGun = (IGun)gunPrefab.GetComponent<FlameThrower>();
    }


}
