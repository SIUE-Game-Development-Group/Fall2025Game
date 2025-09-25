using UnityEngine;

namespace Core.Scripts.Game
{
    /// <summary>
    /// Deals damage to hurtboxes that collide with this.
    /// Hitbox colliders should be triggers.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hitbox : MonoBehaviour
    {
        public float damage;
        
        public void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.TryGetComponent(out Hurtbox hurtbox))
            {
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
    }
}