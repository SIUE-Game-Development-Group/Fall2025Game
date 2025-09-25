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
            if (InputManager.Instance) entity.Move(InputManager.Instance.MoveInput);
        }
    }
}