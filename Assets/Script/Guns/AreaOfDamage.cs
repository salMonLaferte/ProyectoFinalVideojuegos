using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfDamage : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Damage applied to characters in this area per unit of time
    /// </summary>
    float damage;
    [SerializeField]
    /// <summary>
    /// Amount of seconds to pass to active the damage
    /// </summary>
    float unitOfTime;
    [SerializeField]
    /// <summary>
    /// Duration of this area of damage, set to -1 to make it infinite
    /// </summary>
    float timeAlive;
    /// <summary>
    /// Auxiliar timer to know how much time(in seconds) has passed since the last time this area applied damage
    /// </summary>
    float timerForUnit;
    /// <summary>
    /// What characters should take damage from this area
    /// </summary>
    [SerializeField]
    Bullet.AppliesDamageTo appliesDamageTo;
    /// <summary>
    /// List of characters that are in the area of effect
    /// </summary>
    List<Char> charactersInsideTheArea = new List<Char>();

    private void Start()
    {
        if(timeAlive != -1)
            StartCoroutine(DestroyCoroutine());
    }

    private void OnTriggerEnter(Collider collision)
    {
        Char character = collision.gameObject.GetComponent<Char>();
        if (character != null)
        {
            //When character is on the area of effect add it to the list and start a routine to damage him
            if (Char.ShouldTakeDamage(character, appliesDamageTo))
            {
                if (!charactersInsideTheArea.Contains(character))
                {
                    charactersInsideTheArea.Add(character);
                    character.characterDied.AddListener(OnCharacterDied);//Suscribe this object to know if the character died
                    StartCoroutine(DamageCoroutine(character));
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //When character is out of the area of effect remove it from the list
        Char character = collision.gameObject.GetComponent<Char>();
        if (character != null && charactersInsideTheArea.Contains(character))
        {
            charactersInsideTheArea.Remove(character);
        }

    }
    /// <summary>
    /// When a character died remove it from the list
    /// </summary>
    /// <param name="character"></param>
    void OnCharacterDied(Char character)
    {
        charactersInsideTheArea.Remove(character);
    }

    /// <summary>
    /// Coroutine applied to a character to damage iit when is on the area of effect
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    IEnumerator DamageCoroutine(Char character)
    {
        while( charactersInsideTheArea.Contains(character) )
        {
            character.ReceiveDamage(-damage, Bullet.DamageType.Rocket);
            yield return new WaitForSeconds(unitOfTime);
        }
    }
    /// <summary>
    /// Coroutine to destroy this object after the specified time
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(timeAlive);
        GameObject.Destroy(gameObject);
    }
}
