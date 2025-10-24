using System;
using UnityEngine;
using System.Collections.Generic;
using Core.Scripts.Game;
using Features.MainCharacter.Scripts;
using Object = UnityEngine.Object;

public class ItemManager : MonoBehaviour
{
    
    /* TODO:
            1. Make function to swap passives and update the Inventory List accordingly
            2. Rework invenotry system to support multiple weapons and passives rather than set slots
                - Create different lists for weapons/coins, and collected passives/active items
     */
    
    
    // What the player currently holds

    public static List<Item> inventory;

    [SerializeField] static List<Item> AllItems = new List<Item>();

    private string targetDirectoryPath = "Assets/Resources/Weapons";

    // For swaping player's weapon
    GameObject swapItemPrefab;

    public void Start()
    {

        // Set size of array to 3;
        inventory = new List<Item>(new Item[3]); // 0 = item, 1 = passive, 2 = currency

        // Load all items into memory
        LoadItems();

        // If damage totem equipped, buff damage

        GameObject damageTotemInstance = GameObject.Find("DamageTotem");

        if (damageTotemInstance != null)
        {
            Attack_Damage damageTotem = damageTotemInstance.GetComponent<Attack_Damage>();
            Debug.Log("Damage totem equipped on start");
            damageTotem.IncreaseDamageOfWeapon(gameObject.GetComponent<PlayerAttack>().equippedWeapon);
            Debug.Log("Successfully ran increase damage function");
        }

        // Start with random item
        SwapItem(RandomGenerateItem().name);

        // Load weapon after swap
        PlayerAttack playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        inventory[0] = playerAttack.equippedWeapon;
        
        // Load passive slot with nothing
        inventory[1] = new Item("PassiveItemID", "Temp Passive Item", "A placeholder for a passive item", Item.Rarity.Common, 1);

        // Load currency slot with 0
        inventory[2] = new Item("GoldItemID", "Gold", "Player's Currency", Item.Rarity.Common, 0);
        
        
        
    }
    
    // Scan all files in weapon directory and set id = (weapons path)
    public void LoadItems()
    {
        Debug.Log("Scanning for prefabs in directory: " + targetDirectoryPath);
        
        var allAssets = Resources.LoadAll("", typeof(Item));

        Debug.Log($"Found {allAssets.Length} assets in Resources folders:");

        foreach (Object asset in allAssets)
        {
            Debug.Log($"Asset Name: {asset.name}, Type: {asset.GetType().Name}");
            
            Item item = asset as Item;

            if (item == null) return;

            item.id = targetDirectoryPath + "/" + item.name;

            AllItems.Add(item);
        }
    }

    public void SwapItem(string itemName, string beginningFolderName="Weapons/")
    {
        PlayerAttack playerAttackScript = this.gameObject.GetComponent<PlayerAttack>();

        // Find item by name
        Item itemSwap = FindItemByName(itemName);
        if (itemSwap == null)
        {
            Debug.LogWarning("Could not swap item with name: " + itemName);
            return;
        }



        // Load item prefab into unity scene
        swapItemPrefab = Resources.Load<GameObject>(beginningFolderName + itemSwap.name);
        if (swapItemPrefab != null)
        {
            Debug.Log("Successfully loaded swap item");

            playerAttackScript.EquipWeaponFromPrefab(swapItemPrefab.GetComponent<Weapon>());

            // Update inventory weapon slot (Weapon slot only in slot 0)
            inventory[0] = itemSwap;
        }
        else
        {
            Debug.LogWarning("Failed to load prefab from path: " + itemSwap.id);
            return;
        }

        // ~ ~ ~ ~ ~ ~ Apply buffs from totems if they exist ~ ~ ~ ~ ~ ~ ~
       
        // Apply weapon rate buff if it exists
        GameObject weaponRateTotemInstance = GameObject.Find("AttackRateTotem");
        if (weaponRateTotemInstance != null)
        {
            // Run totem rate decrease function with newly equipped weapon
            weaponRateTotemInstance.GetComponent<Attack_Rate>().DecreaseRateOfWeapon(playerAttackScript.equippedWeapon);;
        }

        // Apply weapon damage buff if it exists
        GameObject damageTotemInstance = GameObject.Find("DamageTotem");

        if (damageTotemInstance != null)
        {
            // Run totem damage increase function with newly equipped weapon
            damageTotemInstance.GetComponent<Attack_Damage>().IncreaseDamageOfWeapon(playerAttackScript.equippedWeapon);
        }

    }

    public Item RandomGenerateItem()
    {
        return AllItems[UnityEngine.Random.Range(0, AllItems.Count - 1)];
    }

    // Returns item if found, else returns null
    public Item FindItemByName(string itemName)
    {
        foreach (Item item in AllItems)
        {
            if (item.name == itemName) return item;
        }
        return null;
    }
}