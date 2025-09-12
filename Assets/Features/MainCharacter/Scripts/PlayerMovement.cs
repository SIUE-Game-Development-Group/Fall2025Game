using Core.Scripts.Input;
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