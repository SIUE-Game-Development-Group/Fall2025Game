using System.Collections;
using Core.Scripts.Game;
using Core.Scripts.Input;
using UnityEngine;

namespace Features.Items.Scripts.Weapons
{
    public class TemplateMeleeWeapon : Weapon
    {
        [SerializeField] private float attackDuration = 0.4f;
        [SerializeField] private GameObject hitbox;
        
        
        public void Start()
        {
            hitbox.SetActive(false);
        }

        protected override void Attack()
        {
            StartCoroutine(AttackCoroutine());
        }

        public IEnumerator AttackCoroutine()
        {
            // Get direction to target
            var mousePos = InputManager.Instance.MoveInput;
            var mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
            mousePosWorld.z = 0;
            Vector2 direction = mousePosWorld - transform.position;

            // Calculate the angle in degrees
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            
            // Enable the hitbox gameobject
            hitbox.SetActive(true);
            
            // After attack duration, disable the hitbox
            yield return new WaitForSeconds(attackDuration);
            
            // Disable the hitbox gameobject
            hitbox.SetActive(false);
        }
    }
}