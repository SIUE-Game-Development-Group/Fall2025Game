using UnityEngine;

namespace Core.Scripts.Game
{
    public class Enemy : Entity {
        public void OnEnable()
        {
            OnDeath += OnDefeated;
        }
        
        public void OnDisable()
        {
            OnDeath -= OnDefeated;
        }

        public void OnDefeated()
        {
            Debug.Log("Enemy defeated");
            Destroy(gameObject);
        }
    }
}