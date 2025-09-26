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

        public void EquipDroppedWeapon(DroppedWeapon droppedWeapon)
        {
            // Drop the current weapon if holding one
            if (equippedWeapon)
            {
                var swappedDroppedWeapon = Instantiate(droppedWeaponPrefab).GetComponent<DroppedWeapon>();
                swappedDroppedWeapon.weapon = equippedWeapon;
                swappedDroppedWeapon.UpdateIcon();
                Destroy(equippedWeapon.gameObject);

            }

            // Instantiate the new weapon
            var weapon = Instantiate(droppedWeapon.weapon, transform);
            equippedWeapon = weapon;
            Destroy(droppedWeapon.gameObject);
        }
    }
}