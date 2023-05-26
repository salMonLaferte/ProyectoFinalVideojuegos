using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Implements methods to manage and update the HUD.
/// </summary>
public class HUD : MonoBehaviour
{
    /// <summary>
    /// Image of the armor bar, material needs to have a float percentage parameter
    /// </summary>
    [SerializeField]
    Image armorBarImage;

    Material armorBarMaterial;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    void Start()
    {
        //Suscribe to the armorChanged event for the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        armorBarMaterial = new Material(armorBarImage.material);
        armorBarImage.material = armorBarMaterial;
        if (player != null)
        {
            Player p = player.GetComponent<Player>();
            ChangeArmorBar(p.GetMaxArmor(), p.GetMaxArmor());
            p.armorChanged.AddListener(ChangeArmorBar);
        }

    }
    /// <summary>
    /// Change the material propierty to show the armor bar
    /// </summary>
    /// <param name="armor"></param>
    /// <param name="maxArmor"></param>
    void ChangeArmorBar(float armor, float maxArmor)
    {
        armorBarMaterial.SetFloat("_percentage", armor / maxArmor);
    }

}
