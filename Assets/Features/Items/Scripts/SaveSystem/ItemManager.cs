using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Scripts.Game;
using Features.Items.Scripts.SaveSystem;
using Features.Items.Scripts.Weapons;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;


public class ItemManager : MonoBehaviour
{
    // What the player currently holds
    
    public static List<Item> inventory;


    public static List<Item> AllItems = new List<Item>(); 
    
    private string targetDirectoryPath = "Assets/Resources/Weapons";

    public void Start()
    {
        inventory = new List<Item>();
        
        
        //inventory.Add(new InventoryItem("Copper Sword", 1));
        //inventory.Add(new InventoryItem("Gold Coin", UnityEngine.Random.Range(34, 159)));
        
        LoadItems();
        
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


    public void SwapItem()
    {
        
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