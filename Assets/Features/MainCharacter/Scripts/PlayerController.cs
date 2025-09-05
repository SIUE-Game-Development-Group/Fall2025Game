using Core.Scripts.Input;
using UnityEngine;

namespace Features.MainCharacter.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 move = InputManager.Instance.MoveInput;
            _rb.linearVelocity = move.normalized * moveSpeed;
        }
    }
}