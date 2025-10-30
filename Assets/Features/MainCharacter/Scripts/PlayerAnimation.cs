using System;
using System.Collections.Generic;
using Core.Scripts.Input;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.U2D;

namespace Features.MainCharacter.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private List<Sprite> playerSprite;
        private SpriteRenderer spriteRenderer;
        private Vector2 direction;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            //Zachary's Code

            // Once the player has sprites for each walking direction should probably use an animator and use those sprites instead of rotating the sprite.
            // but until then we'll just rotate the test sprite to show where they're facing
            
            // Get direction to target
            var mousePos = InputManager.Instance.MousePosition;
            mousePos.z = 10f;
            var mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
            mousePosWorld.z = 0;
            direction = mousePosWorld - transform.position;

            // Calculate the angle in degrees
            float angleDirection = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
            angleDirection = Mathf.Repeat(angleDirection, 360);
            int lookDirection;
            switch(angleDirection)
            {
                case <= 22.5f:
                    lookDirection = 0;
                    break;
                case <= 67.5f:
                    lookDirection = 1;
                    break;
                case <= 112.5f:
                    lookDirection = 2;
                    break;
                case <= 157.5f:
                    lookDirection = 3;
                    break;
                case <= 202.5f:
                    lookDirection = 4;
                    break;
                case <= 247.5f:
                    lookDirection = 5;
                    break;
                case <= 292.5f:
                    lookDirection = 6;
                    break;
                case <= 337.5f:
                    lookDirection = 7;
                    break;
                default:
                    lookDirection = 0;
                    break;
            }
            spriteRenderer.sprite = playerSprite[lookDirection];
        }
        public Vector2 GetPlayerDirection()
        {
            return direction;
        }
    }
   
}