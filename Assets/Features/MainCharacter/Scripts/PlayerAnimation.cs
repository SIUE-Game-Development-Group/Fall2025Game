using System;
using Core.Scripts.Input;
using UnityEngine;

namespace Features.MainCharacter.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject playerSprite;

        private void Update()
        {
            // Once the player has sprites for each walking direction should probably use an animator and use those sprites instead of rotating the sprite.
            // but until then we'll just rotate the test sprite to show where they're facing
            
            // Get direction to target
            var mousePos = InputManager.Instance.MousePosition;
            mousePos.z = 10f;
            var mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
            mousePosWorld.z = 0;
            Vector2 direction = mousePosWorld - transform.position;

            // Calculate the angle in degrees
            playerSprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
}