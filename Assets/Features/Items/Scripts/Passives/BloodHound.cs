using System.Collections.Generic;
using Core.Scripts.Game;
using Features.MainCharacter.Scripts;
using UnityEngine;

/*
    NOTES: Increased damage with lower health:
        Increased buff when player is under 50% hp

    TODO:
        - Ensure to reset all weapons original damage before swapping to new one and on totem discard!!
        Bug will occur if you don't reset damage before they drop their weapon for a new one
        
    
*/

public class BloodHound : MonoBehaviour
{

    public float DamageMultiplier = 2.0f;

    List<float> previousDamage;

    GameObject playerObject;

    // Make publically accessible without messing anything up
    [HideInInspector]
    public bool increasedWeaponDamage = false;

    private bool under50;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        previousDamage = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {

        // playerObject.GetComponent<PlayerAttack>().equippedWeapon

        under50 = PlayerUnderHalfHP(playerObject.GetComponent<Entity>());

        // If player health is less than half, remember to only run once until above 50%
        if (under50 && !increasedWeaponDamage)
        {
            IncreaseDamage(playerObject.GetComponent<PlayerAttack>().equippedWeapon);
            increasedWeaponDamage = true;
        }
        else
        {
            if (!under50 && increasedWeaponDamage)
            {
                increasedWeaponDamage = false;
                ResetDamage(playerObject.GetComponent<PlayerAttack>().equippedWeapon);
            }
        }


        // If player is 50% health, reset weapon damage amount to what was before

    }

    public void IncreaseDamage(Weapon weapon)
    {
        // Reset previous damage list
        previousDamage.Clear();

        foreach (var hitbox in weapon.hitboxes)
        {
            // Remember previous damage
            var previous = hitbox.damage;
            previousDamage.Add(hitbox.damage);

            hitbox.damage *= DamageMultiplier;

            Debug.Log($"Increased hitbox {previous} damage by x{DamageMultiplier}");
            Debug.Log($"Damage was {previous} now {hitbox.damage}");
        }
    }

    public void ResetDamage(Weapon weapon)
    {
        for (int i = 0; i < weapon.hitboxes.Length; i++)
        {
            Debug.Log($"Reset damage from {weapon.hitboxes[i].damage} to {previousDamage[i]}");
            weapon.hitboxes[i].damage = previousDamage[i];
        }
    }

    private bool PlayerUnderHalfHP(Entity playerEntity)
    {
        // If player's health less than half of max hp (under 50% hp) return true, otherwise false
        return playerEntity._health < playerEntity._maxHealth * 0.5f;
    }
}
