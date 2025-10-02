using UnityEngine;
using System;
using System.Collections;
using Core.Scripts.Input;

namespace Core.Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] float _maxHealth = 10f;
        [SerializeField] float _moveSpeed = 5f;

        [Tooltip("These hitboxes may be temporarily disabled when taking damage (temporary invincibility)")]
        [SerializeField] private Hurtbox[] _hurtboxes;

        [Tooltip("Invincibility time when taking damage")]
        [SerializeField] private float hitInvincibilityTime = 1.5f;

        /// <summary>
        /// This value is true while the player is invincible due to temporary hit invincibility
        /// </summary>
        /// <returns></returns>
        private bool _hitInvincible;

        Rigidbody2D _rb;

        /// <summary>
        /// current health, set to max health when spawning
        /// </summary>
        float _health = 10;

        /// <summary>
        /// Last move direction that was requested by Move()
        /// </summary>
        Vector2 _moveDirection = Vector2.zero;
    
        /// <summary>
        /// Event invoked when this entity takes damage
        /// </summary>
        public event Action OnDamaged;
    
        /// <summary>
        /// Evet invoked when this entity's health becomes 0 or lower
        /// </summary>
        public event Action OnDeath;

        public virtual void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _health = _maxHealth; // initialize hp to max hp
        }

        public void Update() {
            _rb.linearVelocity = _moveDirection.normalized * _moveSpeed;
        }
    
        public void Move(Vector2 move) {
            _moveDirection = move.normalized;
        }

        public void TakeDamage(float amount) {
            _health -= amount;
            Debug.Log(gameObject.name + " damaged " + amount);
            OnDamaged?.Invoke();

            if (_health <= 0) {
                _health = 0;
                Death();
                OnDeath?.Invoke();
            } else if (!_hitInvincible && hitInvincibilityTime > 0f) {
                StartCoroutine(TemporaryInvincibility());
            }
        }

        public virtual void Death() {

        }

        public IEnumerator TemporaryInvincibility() {
            // Make all hurtboxes invincible
            _hitInvincible = true;
            foreach (var hurtbox in _hurtboxes) {
                hurtbox.StartInvincibility();
            }

            // Wait until not invincible anymore
            yield return new WaitForSeconds(hitInvincibilityTime);

            // Make all hurtboxes not invincible anymore.
            _hitInvincible = false;
            foreach (var hurtbox in _hurtboxes) {
                hurtbox.EndInvincibility();
                // If invincibility down causes taking damage which causes invincibility up, stop the loop
                if (_hitInvincible) break;
            }
        }
    }
}
