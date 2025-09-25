using System;
using Core.Scripts.Utility;
using UnityEngine;

namespace Core.Scripts.Input
{
    public class InputManager : Singleton<InputManager>
    {
        public Vector2 MoveInput { get; private set; }
        public bool AttackJustPressed { get; private set; }
        public bool InteractJustPressed { get; private set; }
        public Vector3 MousePosition { get; private set; }


        private Camera _camera;

        public void Start()
        {
            _camera = Camera.main;
            if (!_camera) Debug.LogWarning("InputManager needs a Camera for mouse input to work properly");
        }

        public void Update()
        {
            MoveInput = new Vector2(
                UnityEngine.Input.GetAxisRaw("Horizontal"),
                UnityEngine.Input.GetAxisRaw("Vertical")
            );

            AttackJustPressed = UnityEngine.Input.GetKeyDown(KeyCode.Space);
            InteractJustPressed = UnityEngine.Input.GetKeyDown(KeyCode.E);
            MousePosition = UnityEngine.Input.mousePosition;
        }
    }
}