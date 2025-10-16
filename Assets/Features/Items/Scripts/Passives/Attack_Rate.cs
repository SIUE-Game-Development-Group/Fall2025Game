using Core.Scripts.Game;
using UnityEngine;

public class Attack_Rate : Item
{
    public float CooldownMultiplier = 1.5f;

    private float previousCooldown;
    private bool previousAssigned = false;

    public void DecreaseRateOfWeapon(Weapon weapon)
    {
        previousCooldown = weapon.cooldown;
        previousAssigned = true;

        weapon.cooldown *= CooldownMultiplier;

        Debug.Log($"Decreased attack rate, was {previousCooldown} now {weapon.cooldown}!");
    }

    // Reset the damage of the hitbox this script is currently buffing
    public void ResetRateOfWeapon(Weapon weapon)
    {
        if (previousAssigned)
        {
            Debug.Log($"Decreased attack rate, was {weapon.cooldown} now {previousCooldown}");
            weapon.cooldown = previousCooldown;
            previousAssigned = false;
        }
    }
}
