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
            Debug.Log(this + " contacting " + hitbox, this);
            _contactingHitboxes.Add(hitbox);
            if (_hittable) entity.TakeDamage(hitbox.damage);
        }

        public void HitboxExited(Hitbox hitbox)
        {
            Debug.Log(this + " no longer contacting " + hitbox, this);
            _contactingHitboxes.Remove(hitbox);
        }

        public void StartInvincibility()
        {
            Debug.Log(this + " is not hittable", this);
            _hittable = false;
        }
    
        public void EndInvincibility()
        {
            Debug.Log(this + " is hittable again", this);
            _hittable = true;
            var _contactingHitboxesCopy = new HashSet<Hitbox>(_contactingHitboxes); // use copy incase contacting hitboxes is modified by defeat
            foreach (var hitbox in _contactingHitboxesCopy)
            {
                if (hitbox.entitiesHit.Contains(entity))
                {
                    Debug.Log("hitbox has already hit " + entity + ", not hitting again");
                    // break;
                }
                hitbox.entitiesHit.Add(entity);
                entity.TakeDamage(hitbox.damage);

                // If taking damage caused this hurtbox to be invincible again,
                // don't check for more hitboxes to hit this
                if (!_hittable) break;
            }
        }
    }
}