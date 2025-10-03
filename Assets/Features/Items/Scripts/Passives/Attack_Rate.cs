using Core.Scripts.Game;
using UnityEngine;

public class Attack_Rate : MonoBehaviour
{
    // Should be whole number
    public float CooldownMultiplier = 1.5f;

    public void Start()
    {
        
    }

    public void DecreaseRateOfWeapon(Weapon weapon)
    {
        float previousCooldown = weapon.cooldown;
        weapon.cooldown /= CooldownMultiplier;

        Debug.Log($"Decreased attack rate, was {previousCooldown} now {weapon.cooldown}!");


    }

    // Reset the damage of the hitbox this script is currently buffing
    public void ResetRateMultOfWeapon(Weapon weapon)
    {
        float previousCooldown = weapon.cooldown;
        weapon.cooldown *= CooldownMultiplier;

        Debug.Log($"Decreased attack rate, was {previousCooldown} now {weapon.cooldown}!");
    }
}
