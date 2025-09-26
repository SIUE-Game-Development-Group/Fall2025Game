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
        
        
        public override void Start()
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
            Vector2 dir = InputManager.Instance.AttackInput;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            angle = (angle + 360) % 360; // Normalize to 0-360

            int sector = Mathf.RoundToInt(angle / 45f) % 8;
            Vector2 dirVector = Directions.Vectors[(Directions.Direction)sector];

            angle = Mathf.Atan2(dirVector.x, dirVector.y) * Mathf.Rad2Deg;

            // Calculate the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            
            // Enable the hitbox gameobject
            hitbox.SetActive(true);
            
            // After attack duration, disable the hitbox
            yield return new WaitForSeconds(attackDuration);
            
            // Disable the hitbox gameobject
            hitbox.SetActive(false);
        }
    }
}