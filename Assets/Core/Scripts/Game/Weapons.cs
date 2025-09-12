using UnityEngine;

public abstract class Weapons : Items
{
    float _damage;
    float _cooldown;

    public abstract void Attack();
    public abstract bool IsOnCooldown();
}
