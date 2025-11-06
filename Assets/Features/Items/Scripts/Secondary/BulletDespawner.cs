using UnityEngine;
using System.Collections;
using Core.Scripts.Game;
public class BulletDespawner : MonoBehaviour
{
    private Hitbox hitbox;
    

    void Start()
    {
        hitbox = GetComponent<Hitbox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent(out Hurtbox hurtbox))
        {
            
            Destroy(transform.root.gameObject);
        }
    }
}

