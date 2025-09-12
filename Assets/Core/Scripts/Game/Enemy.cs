using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    float _health;
    float _damage;

    public abstract float OnAttack();
}
