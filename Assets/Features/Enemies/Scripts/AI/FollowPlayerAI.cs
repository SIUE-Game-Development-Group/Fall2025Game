using UnityEngine;
using Core.Scripts.Game;

namespace Features.Enemies.Scripts.AI
{
    [RequireComponent(typeof(Entity))]
    public class FollowPlayerAI : MonoBehaviour
    {
        private Entity _entity;
        private Transform _playerTransform;

        void Start()
        {
            _entity = GetComponent<Entity>();
            
            // Finds the first GameObject with the "Player" tag
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                _playerTransform = playerObj.transform;
        }

        void Update()
        {
            if (_playerTransform == null)
            {
                _entity.Move(Vector2.zero);
                return;
            }

            // Move towards player
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            _entity.Move(direction);
        }
    }
}