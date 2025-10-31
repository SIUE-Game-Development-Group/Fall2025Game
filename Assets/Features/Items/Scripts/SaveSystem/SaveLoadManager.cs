using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using Core.Scripts.Game;
using Unity.VisualScripting;

public class SaveLoadManager : MonoBehaviour
{
    private string defaultPath;
    public static string weaponSaveName;
    public static string passiveSaveName;
    
    // Path locations
    private string weaponPath;
    private string passiveSavePath;

    [SerializeField] List<string> itemSave = new List<string>();

    public void Start()
    {
        if (defaultPath == null) defaultPath = Application.persistentDataPath;
        if (passiveSaveName == null) passiveSaveName = "/passiveSave.dat";
        if (weaponSaveName == null) weaponSaveName = "/weaponSave.dat";
        weaponPath = defaultPath + weaponSaveName;
        passiveSavePath = defaultPath + passiveSaveName;
    }

    private bool SaveItemNames(List<Item> inventory)
    {
        if (inventory == null)
        {
            Debug.LogError("SaveLoadManager: Inventory is null");
            return false;
        }
        itemSave.Clear();
        itemSave.Add(inventory[0].gameObject.name);
        Debug.Log("Currently saving " + inventory[0].gameObject.name);

        return true;
    }
    
    public void Save()
    {
        // Fallback if not found or tried to call function from other script before start was called
        if (defaultPath == null) defaultPath = Application.persistentDataPath;
        if (passiveSaveName == null) passiveSaveName = "/passiveSave.dat";
        if (weaponSaveName == null) weaponSaveName = "/weaponSave.dat";
        
        weaponPath = defaultPath + weaponSaveName;
        passiveSavePath = defaultPath + passiveSaveName;
        
        ItemManager itemManager = GameObject.FindWithTag("Player").GetComponent<ItemManager>();

        
        BinaryFormatter formatter = new BinaryFormatter();

        // Creating save files at given paths
        FileStream weaponStream = new FileStream(weaponPath, FileMode.Create);
        //FileStream passiveStream = new FileStream(passiveSavePath, FileMode.Create);
        
        // Only returns false if inventory is null
        if (!SaveItemNames(itemManager.inventory)) return;
        
        // Ensure the data we're saving is not null
        if (itemSave == null)
        {
            Debug.LogError("SaveLoadManager: Could not save because itemSave is null!");
            return;
        }
        /*if (itemManager.passives == null)
        {
            Debug.LogError("SaveLoadManager: Could not save because ItemManager.passives is null!");
            return;
        }*/

        // Write out encrypted files
        formatter.Serialize(weaponStream, itemSave);
        //formatter.Serialize(passiveStream, itemManager.passives);

        // Close streams after we're done writing
        weaponStream.Close();
        //passiveStream.Close();

        Debug.Log($"Game Saved! Path: {defaultPath}");
        
    }

    public void Load()
    {

        //string inventoryPath = defaultPath + weaponSaveName;
        string inventoryPath = Application.persistentDataPath + "/weaponSave.dat";
        string passivePath = defaultPath + passiveSaveName; 

        ItemManager itemManager = GameObject.FindWithTag("Player").GetComponent<ItemManager>();

        // Load weapons inventory
        if (File.Exists(inventoryPath))
        {
            // Open file and overwrite inventory list
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(inventoryPath, FileMode.Open);
            itemSave = formatter.Deserialize(stream) as List<string>;

            if (itemSave == null || itemSave[0] == null)
            {
                Debug.LogError("SaveLoadManager: itemSave is null! Cannot load item!");
                return;
            }
            
            // Swap weapons after updating inventory list
            itemManager.SwapItem(itemSave[0]);

            stream.Close();
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.LogWarning("Inventory Save file not found.");
        }
        
        /*
        // Load passives
        if (File.Exists(passivePath))
        {
            // Open file and overwrite passives list
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(passivePath, FileMode.Open);
            itemManager.passives = formatter.Deserialize(stream) as List<Item>;

            // Load saved passives onto player
            foreach (Item passive in itemManager.passives)
            {
                itemManager.AddPassive(passive.gameObject.name);
            }
            
        } else
        {
            Debug.LogWarning("Passive Save file not found.");
        }
        */
    }
}
