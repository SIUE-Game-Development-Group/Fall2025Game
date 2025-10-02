using Core.Scripts.Game;
using Features.Items.Scripts.Weapons;
using UnityEngine;

public class Attack_Damage : MonoBehaviour
{
    // Should be whole number
    public float PercentIncrease;

    private GameObject player;
    private GameObject equippedWeapon;
    private Hitbox weaponHitbox;

    // Used for tracking item's base damage before buff
    private float itemOriginalDamage = -1f;

    public void IncreaseDamage()
    {
        // Grab GameObject of item through player
        player = GameObject.FindWithTag("Player");
        equippedWeapon = player.GetComponentInChildren<TemplateMeleeWeapon>().gameObject;
        weaponHitbox = equippedWeapon.transform.Find("AttackHitbox").gameObject.GetComponent<Hitbox>();

        // Reset weapon back to normal
        ResetDamageMult();

        // Used for when we revert to normal after passive is gone
        if (itemOriginalDamage == -1f) itemOriginalDamage = weaponHitbox.damage;

        weaponHitbox.damage *= PercentIncrease;

        Debug.Log($"Increased weapon {equippedWeapon.name} damage by x{PercentIncrease}");
        Debug.Log($"Damage was {itemOriginalDamage} now {weaponHitbox.damage}");
    }
    // Call this when item etiher destroyed or "dropped"
    public void ResetDamageMult()
    {
        if (weaponHitbox != null && itemOriginalDamage != -1f)
        {
            weaponHitbox.damage = itemOriginalDamage;
            itemOriginalDamage = -1f;
        }
        
    }

    // When no longer equipped/destroyed, reset damage back to normal
    public void OnDestroy()
    {
        ResetDamageMult();
    }
}
