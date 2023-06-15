using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
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

    [HideInInspector]
    protected GameObject currentGunObject;

    [SerializeField]
    protected GameObject healthBarPrefab;
    
    protected bool damageCoroutineActive = false;

    public UnityEvent<Char> characterDied = new UnityEvent<Char>();
    
    [HideInInspector]
    public UnityEvent<float,float> armorChanged = new UnityEvent<float,float>();

    [SerializeField] 
    AudioClip hurt;

    [SerializeField]
    GameObject deathParticles;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected virtual void Awake(){
        if(healthBarPrefab !=null ){
             GameObject bar = GameObject.Instantiate(healthBarPrefab, transform.position + new Vector3(0,6,0), Quaternion.identity);
             bar.GetComponent<ArmorBar>().characterRef = this;
             bar.transform.SetParent(transform);
        }
        ChangeGun(GetInitialGunName());
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected virtual void Start(){
        movingTo = transform.position;
        stateMachine = new CharStateMachine(new CharIddle());
        armor = maxArmor;
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected virtual void Update()
    {
        //updates the current state in the state machine
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
            rb.velocity = new Vector3(rb.velocity.x, -9, rb.velocity.z);
        }
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="col"></param>
    protected virtual void OnCollisionEnter(Collision col)
    {

    }

    /// <summary>
    /// Tracks where in the ground is the mouse pointing and returns that position.
    /// </summary>
    /// <returns>The position of the ground the mouse is pointing to</returns>
    protected Vector3 GetSelectedGroundPoint(){
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(r, out hit, 1000, GroundLayer);
        return hit.point;
    }
    /// <summary>
    /// Stuns the character for the specified amount of time
    /// </summary>
    /// <param name="time"></param>
    public void Stun(float time){
        if(!(stateMachine.GetCurrent() is CharStunned)){
            StartCoroutine(StunCoroutine(time));
        }
    }
    /// <summary>
    /// Coroutine that changes the character state to a stun state.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator StunCoroutine(float time){
        stateMachine.ChangeState( new CharStunned() );
        yield return new WaitForSeconds(time);
        stateMachine.StopStun();
    }

    /// <summary>
    /// Modify the armor by the specified amount,
    /// amount can be positive or negative
    /// </summary>
    /// <param name="amount"></param>
    public virtual void ModifyArmor(float amount){
        armor += amount;
        if( armor > maxArmor)
            armor = maxArmor;
        if(armor < 0)
            armor = 0;
        if (hurt != null && amount <0)
        {
            GameObject audioSource = new GameObject();
            audioSource.AddComponent<AudioSource>();
            GameObject.Instantiate(audioSource, transform.position, Quaternion.identity);
            audioSource.GetComponent<AudioSource>().clip = hurt;
            audioSource.GetComponent<AudioSource>().Play();
        }
            
        armorChanged.Invoke(armor, maxArmor);
        if(amount < 0 && !damageCoroutineActive)
            StartCoroutine(ChangeMaterialForDamage());
        if(armor <= 0){
            characterDied.Invoke(this);
            if( gameObject.GetComponent<Player>() == null)
            {
                GameObject.Destroy(gameObject);
                if(deathParticles != null)
                {
                    GameObject.Instantiate(deathParticles, transform.position, Quaternion.identity);
                }
            }
                
        }
    }

    public virtual void ReceiveDamage(float amount, Bullet.DamageType damageType)
    {
        ModifyArmor(amount);
    }

    /// <summary>
    /// Coroutine that makes the material blink when taking damage.
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeMaterialForDamage(){
        damageCoroutineActive = true;
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        for(int i=0; i< meshes.Length; i++){
            if(meshes[i] != null)
                meshes[i].material.SetInt("_Hurt", 1);
        }
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            if(skinnedMeshRenderers[i] != null)
                skinnedMeshRenderers[i].material.SetInt("_Hurt", 1);
        }
        yield return new WaitForSeconds(.1f);
        for(int i=0; i<meshes.Length; i++){
            if (meshes[i] != null)
                meshes[i].material.SetInt("_Hurt", 0);
        }
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            if (skinnedMeshRenderers[i] != null)
                skinnedMeshRenderers[i].material.SetInt("_Hurt", 0);
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
    /// Returns the max armor of the character.
    /// </summary>
    /// <returns></returns>
    public float GetMaxArmor(){
        return maxArmor;
    }

    /// <summary>
    /// Restores an amount of armor.
    /// </summary>
    public void RestoreArmor(){
        ModifyArmor(maxArmor);
    }

    /// <summary>
    /// Respawns the character in a determined position.
    /// </summary>
    /// <param name="position"></param>
    public void RespawnOnPoint(Vector3 position){
        RestoreArmor();
        stateMachine.ChangeState(new CharIddle());
        transform.position = position;
    }

    /// <summary>
    /// Change the gun of the character.
    /// </summary>
    /// <param name="name"></param>
    public virtual void ChangeGun(string name)
    {
        if (name == "none")
            return;
        GameObject gunPrefab = GameObject.Instantiate((UnityEngine.GameObject)Resources.Load(name), gunPosition.transform.position, transform.rotation);
        gunPrefab.transform.SetParent(transform);
        GameObject.Destroy(currentGunObject);
        currentGunObject = gunPrefab;
        if (name == "Pistol" || name == "PistolShortRange")
            currentGun = (IGun)gunPrefab.GetComponent<Pistol>();
        if (name == "RocketLauncher")
            currentGun = (IGun)gunPrefab.GetComponent<Bazooka>();
        if (name == "FlameThrower")
            currentGun = (IGun)gunPrefab.GetComponent<FlameThrower>();
    }

    /// <summary>
    /// Shoots the current weapon.
    /// </summary>
    /// <param name="point"></param>
    public virtual void Shoot(Vector3 point)
    {
        stateMachine.ChangeState(new CharShooting(point, this));
    }

    /// <summary>
    /// Returns the current gun of the player
    /// </summary>
    /// <returns></returns>
    public IGun GetGun()
    {
        return currentGun;
    }

    /// <summary>
    /// Get the initial gun name for the character, the one that uses starting the scene.
    /// </summary>
    /// <returns></returns>
    public virtual string GetInitialGunName()
    {
        return "Pistol";
    }

    /// <summary>
    /// Gets the movement speed of the character.
    /// </summary>
    /// <returns></returns>
    public float GetSpeed()
    {
        return speed;
    }

    /// <summary>
    /// Get the current armor of the character.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentArmor()
    {
        return armor;
    }

}
