using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using Core.Scripts.Game;
using Unity.VisualScripting;

public class SaveLoadManager : MonoBehaviour
{
    /*
        TODO:
            - On load, make sure to replace current weapon player is holding using
            SwapItem function in ItemManager script!`
    */

    private string defaultPath;
    public static string weaponSaveName;
    public static string passiveSaveName;

    public void Start()
    {
        defaultPath = Application.persistentDataPath;
        passiveSaveName = "/passiveSave.dat";
        weaponSaveName = "/weaponSave.dat";
    }

    public void Save()
    {
        // Fallback if not found or tried to call function from other script before start was called
        if (defaultPath == null) defaultPath = Application.persistentDataPath;
        if (passiveSaveName == null) passiveSaveName = "/passiveSave.dat";
        if (weaponSaveName == null) weaponSaveName = "/weaponSave.dat";

        ItemManager itemManager = GameObject.FindWithTag("Player").GetComponent<ItemManager>();

        Debug.Log($"Save path: {defaultPath}/...");

        // Path locations
        string weaponPath = defaultPath + weaponSaveName;
        string passiveSavePath = defaultPath + passiveSaveName;

        BinaryFormatter formatter = new BinaryFormatter();

        // Creating save files at given paths
        FileStream weaponStream = new FileStream(weaponPath, FileMode.Create);
        FileStream passiveStream = new FileStream(passiveSavePath, FileMode.Create);

        // Ensure the data we're saving is not null
        if (itemManager.inventory == null)
        {
            Debug.LogError("SaveLoadManager: Could not save because ItemManager.inventory is null!");
            return;
        }
        if (itemManager.passives == null)
        {
            Debug.LogError("SaveLoadManager: Could not save because ItemManager.passives is null!");
            return;
        }

        // Write out encrypted files
        formatter.Serialize(weaponStream, itemManager.inventory);
        formatter.Serialize(passiveStream, itemManager.passives);

        // Close streams after we're done writing
        weaponStream.Close();
        passiveStream.Close();

        Debug.Log("Game Saved!");
    }

    public void Load()
    {

        string inventoryPath = defaultPath + weaponSaveName;
        string passivePath = defaultPath + passiveSaveName; 

        ItemManager itemManager = GameObject.FindWithTag("Player").GetComponent<ItemManager>();

        // Load weapons inventory
        if (File.Exists(inventoryPath))
        {
            // Open file and overwrite inventory list
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(inventoryPath, FileMode.Open);
            itemManager.inventory = formatter.Deserialize(stream) as List<Item>;

            // Swap weapons after updating inventory list
            itemManager.SwapItem(itemManager.inventory[0].name);

            stream.Close();
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.LogWarning("Inventory Save file not found.");
        }
        
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
    }
}
