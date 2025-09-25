using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Scripts.Game
{
    /// <summary>
    /// The area of an entity that can take damage.
    /// Hurtbox colliders should be triggers.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hurtbox : MonoBehaviour
    {
        [SerializeField] private Entity entity;
        public Entity Entity => entity;

        /// <summary>
        /// Hitboxes this hurtbox is currently in contact with
        /// </summary>
        private HashSet<Hitbox> _contactingHitboxes = new();

        /// <summary>
        /// If false, this hitbox cannot take damage.
        /// </summary>
        private bool _hittable = true;

        public void HitboxEntered(Hitbox hitbox) 
        {
            _contactingHitboxes.Add(hitbox);
            if (_hittable) entity.TakeDamage(hitbox.damage);
        }

        public void HitboxExited(Hitbox hitbox)
        {
            _contactingHitboxes.Remove(hitbox);
        }

        private void OnDisable()
        {
            // _contactingHitboxes.Clear();
        }

        public void StartInvincibility()
        {
            _hittable = false;
        }
    
        public void EndInvincibility()
        {
            _hittable = true;
            foreach (var hitbox in _contactingHitboxes)
            {
                if (hitbox.entitiesHit.Contains(entity)) return;
                hitbox.entitiesHit.Add(entity);
                entity.TakeDamage(hitbox.damage);
                
                // If taking damage caused this hurtbox to be invincible again,
                // don't check for more hitboxes to hit this
                if (!_hittable) break;
            }
        }
    }
}