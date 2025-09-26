using UnityEngine;
using System.Collections.Generic;

namespace Core.Scripts.Game
{
    /// <summary>
    /// Deals damage to hurtboxes that collide with this.
    /// Hitbox colliders should be triggers.
    /// </summary>
    public class Hitbox : MonoBehaviour
    {
        public float damage;

        public HashSet<Entity> entitiesHit = new();

        /// <summary>
        /// If true, this hitbox can only hit a hurtbox once and then it disables itself until re-enabled. 
        /// Etc. hitboxes on a sword weapon should hit only once
        /// </summary>
        public bool hitOnce = false;
        
        public void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.TryGetComponent(out Hurtbox hurtbox))
            {
                // If this hitbox has already hit the target entity during this attack, don't hit them again until the next attack is used and the entity is cleared from this set
                if (hitOnce && entitiesHit.Contains(hurtbox.Entity)) return;
                entitiesHit.Add(hurtbox.Entity);

                // Will deal damage immediately if the hurtbox is not currently invincible.
                // If it is currently invincible, will only deal damage when it loses invincibility and still contacting
                hurtbox.HitboxEntered(this);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Hurtbox hurtbox))
            {
                hurtbox.HitboxExited(this);
            }
        }

        /// <summary>
        /// Call when the attack starts so that it can hit any enemy only once
        /// </summary>
        public void ResetHit() {
            entitiesHit.Clear();
        }
    }
}