using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Entity _entity;

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent<Hurtbox>(out Hurtbox hb)) {
            _entity.TakeDamage(hb.damage);
        }
    }
}
