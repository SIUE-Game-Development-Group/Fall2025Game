using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Scripts.Game;
using Features.Items.Scripts.SaveSystem;
using Features.Items.Scripts.Weapons;
using Features.MainCharacter.Scripts;
using Mono.Cecil;
using UnityEditor.UIElements;
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
        inventory = new List<Item>();
        
        //inventory.Add(new InventoryItem("Copper Sword", 1));
        //inventory.Add(new InventoryItem("Gold Coin", UnityEngine.Random.Range(34, 159)));
        
        LoadItems();
        
        SwapItem("a");
        
        RandomGenerateItem();
        
    }

    void Update()
    {

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
            
            item.id = targetDirectoryPath + "/"  + item.name;

            AllItems.Add(item);

        }
    }


    public void SwapItem(string itemName)
    {

        GameObject createdItemObject;
        
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
        } else
        {
            Debug.LogWarning("Failed to load prefab from path: " + itemSwap.id);
            return;
        }
        

    }



    public void RandomGenerateItem()
    {
        inventory.Add(AllItems[UnityEngine.Random.Range(0, AllItems.Count-1)]);
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