using System;
using Core.Scripts.Game;
using Core.Scripts.Input;
using UnityEngine;

namespace Features.MainCharacter.Scripts
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Weapon equippedWeapon;
        
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
    }
}