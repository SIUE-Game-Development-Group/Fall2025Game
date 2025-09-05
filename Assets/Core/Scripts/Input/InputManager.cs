using Core.Scripts.Utility;
using UnityEngine;

namespace Core.Scripts.Input
{
    public class InputManager : Singleton<InputManager>
    {
        public Vector2 MoveInput { get; private set; }
        public bool AttackPressed { get; private set; }
        public bool InteractPressed { get; private set; }

        private void Update()
        {
            MoveInput = new Vector2(
                UnityEngine.Input.GetAxisRaw("Horizontal"),
                UnityEngine.Input.GetAxisRaw("Vertical")
            );

            AttackPressed = UnityEngine.Input.GetKeyDown(KeyCode.Space);
            InteractPressed = UnityEngine.Input.GetKeyDown(KeyCode.E);
        }
    }
}