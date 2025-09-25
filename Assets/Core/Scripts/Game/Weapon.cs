using UnityEngine;

public abstract class Weapon : Item
{
    float _damage;
    float _cooldown;

    public abstract void Attack();

    public bool IsOnCooldown()
    {
        // TODO: implement cooldown timer
        return false;
    }
}
