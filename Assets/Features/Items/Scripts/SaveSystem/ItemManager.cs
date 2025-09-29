using UnityEngine;
using System.Collections.Generic;
using Core.Scripts.Game;
using Features.MainCharacter.Scripts;
using Object = UnityEngine.Object;

public class ItemManager : MonoBehaviour
{
    // What the player currently holds

    public static List<Item> inventory;

    public static List<Item> AllItems = new List<Item>();

    private string targetDirectoryPath = "Assets/Resources/Weapons";

    // For swaping player's weapon
    GameObject swapItemPrefab;
    Transform parentObjectTransform;

    public void Start()
    {

        // Set size of array to 3;
        inventory = new List<Item>(new Item[3]); // 0 = item, 1 = passive, 2 = currency

        // Load all items into memory
        LoadItems();

        // Start with random item
        SwapItem(RandomGenerateItem().name);

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

    public void SwapItem(string itemName)
    {
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

            PlayerAttack playerAttackScript = this.gameObject.GetComponent<PlayerAttack>();
            playerAttackScript.EquipWeapon(swapItemPrefab.GetComponent<Weapon>());

            // Update inventory weapon slot (Weapon slot only in slot 0)
            inventory[0] = itemSwap;
        }
        else
        {
            Debug.LogWarning("Failed to load prefab from path: " + itemSwap.id);
            return;
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