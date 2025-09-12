using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    float _health;
    float _speed;
    Rigidbody2D _rb;
    Hitbox _hb;

    public abstract void Die();
    public abstract void TakeDamage(float p_damage);

    public abstract void Move();
}
