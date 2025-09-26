using System;
using Core.Scripts.Game;
using Core.Scripts.Input;
using UnityEngine;

namespace Features.MainCharacter.Scripts
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Weapon equippedWeapon;

        [Header("Prefabs")]
        [SerializeField] private DroppedWeapon droppedWeaponPrefab;
        
        private void Update()
        {
            if (InputManager.Instance && InputManager.Instance.AttackJustPressed)
            {
                if (equippedWeapon)
                {
                    equippedWeapon.AttackIfReady();
                }
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            // Drop the current weapon if holding one
            if (equippedWeapon)
            {
                var swappedDroppedWeapon = Instantiate(droppedWeaponPrefab).GetComponent<DroppedWeapon>();
                swappedDroppedWeapon.weapon = equippedWeapon;
                swappedDroppedWeapon.UpdateIcon();
                Destroy(equippedWeapon.gameObject);

            }
            
            // Instantiate a new copy of the weapon onto the player
            var spawnedWeapon = Instantiate(weapon, transform);
            equippedWeapon = spawnedWeapon;
            
        }

        public void EquipDroppedWeapon(DroppedWeapon droppedWeapon)
        {
            EquipWeapon(droppedWeapon.weapon);
            Destroy(droppedWeapon.gameObject);
        }
    }
}