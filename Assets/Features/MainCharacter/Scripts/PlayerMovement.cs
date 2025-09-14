using Core.Scripts.Input;
using Core.Scripts.Game;
using UnityEngine;

namespace Features.MainCharacter.Scripts
{
    
    public class PlayerMovement : MonoBehaviour
    {
        public Entity entity;

        public void Update() {
            entity.Move(InputManager.Instance.MoveInput);
        }
    }
}