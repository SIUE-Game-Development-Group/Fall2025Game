using Core.Scripts.Game;
using Features.MainCharacter.Scripts;
using UnityEngine;

public class DroppedWeapon : Interactable {
    public SpriteRenderer iconSpriteRenderer;

    /// <summary>
    /// Weapon prefab to put on the player when this item is interacted to equip the weapon
    /// </summary>
    public Weapon weaponPrefab;

    public void UpdateIcon()
    {
        iconSpriteRenderer.sprite = weaponPrefab.icon;
    }

    public override void Interact()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        var playerAttack = playerObj.GetComponent<PlayerAttack>();
        playerAttack.EquipDroppedWeapon(this);
    }
}