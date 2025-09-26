using System;
using Core.Scripts.Input;
using Core.Scripts.Game;
using UnityEngine;

namespace Features.MainCharacter.Scripts
{
    
    public class PlayerMovement : MonoBehaviour
    {
        public Entity entity;

        private void Start()
        {
            if (!InputManager.Instance)
            {
                Debug.LogWarning("PlayerMovement requires an InputManager in the scene to work properly!");
            }
        }

        public void Update()
        {
            if (InputManager.Instance) entity.Move(GetMoveAngle(InputManager.Instance.MoveInput));
        }

        private Vector2 GetMoveAngle(Vector2 InputDir)
        {
            Vector2 dir = InputDir;

            if (dir == Vector2.zero)
            {
                return dir;
            }

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            angle = (angle + 360) % 360; // Normalize to 0-360

            int sector = Mathf.RoundToInt(angle / 45f) % 8;
            Vector2 dirVector = Directions.Vectors[(Directions.Direction)sector];
            Debug.DrawRay(this.transform.position, dirVector * 2f, Color.red);
            return dirVector;
        }
    }
}