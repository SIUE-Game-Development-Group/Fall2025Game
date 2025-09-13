using UnityEngine;
using System;
using Core.Scripts.Input;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] float _maxHealth = 10;
    [SerializeField] float _speed = 5f;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Hitbox _hb;

    /// <summary>
    /// current health
    /// </summary>
    float _health = 10;

    /// <summary>
    /// Last move direction that was requested
    /// </summary>
    Vector2 _move;

    public void Die() {

    }

    public void Move(Vector2 move) {
        _move = move;
    }

    public event Action OnDamaged;

    public event Action OnDeath;

    private int _currentHealth;

    private void Awake() {
        _health = _maxHealth;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _rb.linearVelocity = _move.normalized * _speed;
    }

    public virtual void TakeDamage(float amount) {
        _health -= amount;
        OnDamaged?.Invoke();
        if (_currentHealth <= 0) {
            _currentHealth = 0;
            OnDeath?.Invoke();
        }
    }
}
