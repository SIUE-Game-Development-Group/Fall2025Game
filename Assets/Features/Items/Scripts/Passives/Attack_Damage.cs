using Core.Scripts.Game;
using Features.Items.Scripts.Weapons;
using UnityEngine;

public class Attack_Damage : MonoBehaviour
{
    // Should be whole number
    public float DamageMultiplier = 1.5f;
    
    public void IncreaseDamageOfWeapon(Weapon weapon)
    {
        foreach (var hitbox in weapon.hitboxes)
        {
            var previousDamage = hitbox.damage;
            hitbox.damage *= DamageMultiplier;

            Debug.Log($"Increased hitbox {hitbox} damage by x{DamageMultiplier}");
            Debug.Log($"Damage was {previousDamage} now {hitbox.damage}");
        }
    }

    // Reset the damage of the hitbox this script is currently buffing
    public void ResetDamageMultOfWeapon(Weapon weapon)
    {
        foreach (var hitbox in weapon.hitboxes)
        {
            var previousDamage = hitbox.damage;
            hitbox.damage /= DamageMultiplier;

            Debug.Log($"Reset hitbox {hitbox} damage");
            Debug.Log($"Damage was {previousDamage} now {hitbox.damage}");
        }
    }
}
