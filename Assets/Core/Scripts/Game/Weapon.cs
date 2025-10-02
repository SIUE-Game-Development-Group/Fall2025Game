using System;
using UnityEngine;

namespace Core.Scripts.Game
{
    /// <summary>
    /// A weapon gameobject that can be parented under the player and can call Attack() to use its attack when not on cooldown.
    /// </summary>
    public abstract class Weapon : Item
    {
        [SerializeField] private float cooldown;
        [SerializeField] public Hitbox[] hitboxes;
        
        private float _cooldownTimer;

        /// <summary>
        /// Override this to define what this weapon does when attack is pressed!
        /// </summary>
        protected abstract void Attack();

        public virtual void Start()
        {
            _cooldownTimer = cooldown;
        }

        private void Update()
        {
            if (IsOnCooldown()) _cooldownTimer -= Time.deltaTime;
        }

        public bool IsOnCooldown()
        {
            return _cooldownTimer > 0;
        }

        public void AttackIfReady()
        {
            if (IsOnCooldown()) return;
            _cooldownTimer = cooldown;
            foreach (var hitbox in hitboxes)
                hitbox.ResetHit();
            Attack();
        }
    }
}