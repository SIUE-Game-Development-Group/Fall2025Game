using System;
using UnityEngine;

namespace Features.Combat.Scripts
{
    public class Health : MonoBehaviour
    {
        public event Action OnDamaged;
        
        public event Action OnDeath;
        
        [SerializeField] private int maxHealth = 100;

        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            OnDamaged?.Invoke();
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnDeath?.Invoke();
            }
        }
    }
}