using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using Core.Scripts.Game;

/*
    TODO:
        - Remove already equipped passives after loading new ones in from save
*/

public class SaveLoadManager : MonoBehaviour
{
    private string defaultPath;
    public static string weaponSaveName;
    public static string passiveSaveName;

    // Path locations
    private string weaponSavePath;
    private string passiveSavePath;

    [SerializeField] List<string> itemSave = new List<string>();
    [SerializeField] List<string> passiveSave = new List<string>();

    public void Start()
    {
        defaultPath = Application.persistentDataPath;
        
        passiveSaveName = "/passiveSave.dat";
        weaponSaveName = "/weaponSave.dat";
        
        weaponSavePath = defaultPath + weaponSaveName;
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

    private bool SavePassiveNames(List<Item> passives)
    {
        if (passives == null)
        {
            Debug.LogError("SaveLoadManager: Passives inventory is null");
            return false;
        }

        foreach (Item passive in passives)
        {
            if (passive != null)
            {
                passiveSave.Add(passive.gameObject.name);
                Debug.Log("Currently saving " + passive.gameObject.name);
            }
        }
        return true;

    }

    public void Save()
    {
        Start();

        ItemManager itemManager = GameObject.FindWithTag("Player").GetComponent<ItemManager>();


        BinaryFormatter formatter = new BinaryFormatter();

        // Creating save files at given paths
        FileStream weaponStream = new FileStream(weaponSavePath, FileMode.Create);
        FileStream passiveStream = new FileStream(passiveSavePath, FileMode.Create);

        // Only returns false if inventory is null
        if (!SaveItemNames(itemManager.inventory)) return;
        SavePassiveNames(itemManager.passives);

        // Ensure the data we're saving is not null
        if (itemSave == null)
        {
            Debug.LogError("SaveLoadManager: Could not save because itemSave is null!");
            return;
        }
        if (passiveSave == null)
        {
            Debug.LogWarning("SaveLoadManager: Could not save passives because passiveSave is null!");
        }

        // Write out encrypted files
        formatter.Serialize(weaponStream, itemSave);
        formatter.Serialize(passiveStream, passiveSave);

        // Close streams after we're done writing
        weaponStream.Close();
        passiveStream.Close();

        Debug.Log($"Game Saved! Path: {defaultPath}");

    }

    public void Load()
    {
        Start();

        ItemManager itemManager = GameObject.FindWithTag("Player").GetComponent<ItemManager>();

        // Load weapons inventory
        if (File.Exists(weaponSavePath))
        {
            // Open file and overwrite inventory list
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(weaponSavePath, FileMode.Open);
            itemSave = formatter.Deserialize(stream) as List<string>;

            if (itemSave == null || itemSave[0] == null)
            {
                Debug.LogError("SaveLoadManager: itemSave is null! Cannot load item!");
                return;
            }

            // Swap weapons after updating inventory list
            itemManager.SwapItem(itemSave[0]);

            stream.Close();

            Debug.Log("Loaded all weapons!");
        }
        else
        {
            Debug.LogError("Inventory Save file not found.");
        }

        // Load passives
        if (File.Exists(passiveSavePath))
        {
            // Open file and overwrite passives list
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(passiveSavePath, FileMode.Open);
            passiveSave = formatter.Deserialize(stream) as List<string>;

            itemManager.passives.Clear();

            // Load saved passives onto player
            foreach (string passive in passiveSave)
            {
                itemManager.AddPassive(passive);
            }

            stream.Close();

            Debug.Log("Loaded all passives!");

        }
        else
        {
            Debug.LogError("Passive Save file not found.");
        }

        Debug.Log("Loaded all saves!");
    }
}
