using System;
using UnityEngine;
using System.Collections.Generic;
using Core.Scripts.Game;
using Features.MainCharacter.Scripts;
using Object = UnityEngine.Object;
using UnityEngine.InputSystem.Processors;
using Unity.VisualScripting;

public class ItemManager : MonoBehaviour
{

    /* TODO:
            1. Make function to swap passives and update the Inventory List accordingly
            2. Rework invenotry system to support multiple weapons and passives rather than set slots
                - Create different lists for weapons/coins, and collected passives/active items
     */


    // What the player currently holds

    public List<Item> inventory;
    public List<Item> passives;

    public static List<Item> AllItems = new List<Item>();

    private string targetDirectoryPath = "Assets/Resources/Weapons";

    // For swaping player's weapon
    GameObject swapItemPrefab;

    public void Start()
    {
        // Set size of array to 2
        inventory = new List<Item>(new Item[2]); // 0 = item, 1 = currency

        passives = new List<Item>();

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
        
        // Give player passives on start (REMOVE WHEN DONE TESTING)
        AddPassive("BloodHoundTotem");
        AddPassive("CrystalLeech");
        
        // Test save items on start (REMOVE WHEN DONE TESTING)
        SaveLoadManager manager = new SaveLoadManager();
        manager.Save();

    }

    // Scan all files in weapon directory and set id = (weapons path)
    public void LoadItems()
    {
        var allAssets = Resources.LoadAll("", typeof(Item));

        foreach (Object asset in allAssets)
        {
            Debug.Log($"Asset Name: {asset.name}, Type: {asset.GetType().Name}");

            Item item = asset as Item;

            if (item == null) return;

            item.id = targetDirectoryPath + "/" + item.name;

            AllItems.Add(item);
        }
    }
    
    // Add passive by string name of actual item (NOT by item name but gameobject name!!!!)
    public void AddPassive(string itemName)
    {
        // Find item by name and make sure it exists
        Item passiveItem = FindItemByName(itemName);
        if (passiveItem == null)
        {
            Debug.LogError("ItemManager - AddPassive: Item not found!");
            return;
        }

        // Create item in scene
        GameObject instantiatedPassive = Instantiate(passiveItem.gameObject);
        // Remove "clone" suffix
        instantiatedPassive.name = instantiatedPassive.name.Replace("(Clone)", "");

        // Apply newly created item to player
        instantiatedPassive.transform.parent = GameObject.FindWithTag("Player").transform;

        // Update list of passives
        passives.Add(passiveItem);
    }

    public void SwapItem(string itemName)
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
        swapItemPrefab = Resources.Load<GameObject>("Weapons/" + itemSwap.name);
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
            weaponRateTotemInstance.GetComponent<Attack_Rate>().DecreaseRateOfWeapon(playerAttackScript.equippedWeapon); ;
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
    public static Item FindItemByName(string itemName)
    {
        foreach (Item item in AllItems)
        {
            if (item.name == itemName) return item;
        }
        return null;
    }
}